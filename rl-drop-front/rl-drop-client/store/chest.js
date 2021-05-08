export const state = () => ({
  chests: null,
  currentChest: null,
  winItem: null
})

export const mutations = {
  setChests (state, chests) {
    state.chests = chests
  },
  deleteChestById (state, id) {
    state.chests = state.chests.filter((item) => {
      return item.id !== id
    })
  },
  setCurrentChest (state, chest) {
    state.currentChest = chest
  },
  setWinItem (state, item) {
    state.winItem = item
  }
}

export const actions = {
  async loadAllIndexPage ({ commit, rootGetters }) {
    try {
      this.$axios.setToken(rootGetters.getToken, 'Bearer')
      const result = await this.$axios.$get(`${this.$axios.defaults.baseURL}/chests-list`)
      commit('setChests', result.data.chests)
      commit('banners/setTopIndexBanner', result.data.indexTopBanner, { root: true })
      commit('banners/setBottomIndexBanner', result.data.indexBottomBanner, { root: true })
    } catch (e) {
      throw e.response
    }
  },
  async loadItemsForChest ({ commit, rootGetters }, id) {
    try {
      this.$axios.setToken(rootGetters.getToken, 'Bearer')
      const result = await this.$axios.$get(`${this.$axios.defaults.baseURL}/chest/${id}`)
      let items = []
      for (const item in result.data.items) {
        items = items.concat(result.data.items[item])
      }
      result.data.items = items
      delete result.data.chest.items
      commit('setCurrentChest', result.data)
    } catch (e) {
      throw e.response
    }
  },
  async openChest ({ commit, rootGetters, getters }, data) {
    try {
      this.$axios.setToken(rootGetters.getToken, 'Bearer')
      const result = await this.$axios.$post(`${this.$axios.defaults.baseURL}/open-chest`, data)
      if (result.success) {
        commit('setWinItem', result.data)
        commit('user/changeUserBalance', getters.getCurrentChest.chest[`${rootGetters['common/getPlatform']}Price`] * -1, { root: true })
        commit('user/beautifyBalance', '', { root: true })
      }
      return result
    } catch (e) {
      throw e.response
    }
  }
}

export const getters = {
  getAllChests: state => state.chests,
  getCurrentChest: state => state.currentChest,
  getWinItem: state => state.winItem
}
