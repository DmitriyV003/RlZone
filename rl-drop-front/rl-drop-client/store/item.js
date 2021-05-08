export const state = () => ({
  winLiveItems: null,
  currentCraftItem: null,
  btnDisabled: true,
  types: [],
  craftItems: []
})

export const mutations = {
  setLiveItems (state, items) {
    state.winLiveItems = items
  },
  setCurrentCraftItem (state, items) {
    state.currentCraftItem = items
  },
  setBtnState (state, btn) {
    state.btnDisabled = btn
  },
  setTypes (state, types) {
    state.types = types
  },
  updateLiveItems (state, item) {
    state.winLiveItems.push(item)
  },
  setCraftItems (state, items) {
    state.craftItems = items
  }
}

export const actions = {
  SOCKET_setLiveItems ({ commit }, items) {
    commit('setLiveItems', items)
  },
  async fetchCraftItems ({ commit }) {
    try {
      const result = await this.$axios.$get(`${this.$axios.defaults.baseURL}/craft-items`)
      commit('setTypes', result.data.types)
      commit('setCraftItems', result.data.items)
    } catch (e) {
      throw e.response
    }
  },
  async getItemForCraft ({ commit }, id) {
    try {
      const result = await this.$axios.$get(`${this.$axios.defaults.baseURL}/craft-item/${id}`)
      if (result.success) {
        commit('setCurrentCraftItem', result.data)
        // eventBus.$emit('setPrice', true)
      }
      return result
    } catch (e) {
      throw e.response
    }
  },
  async play ({ commit, rootGetters, getters }, progress) {
    try {
      const data = new FormData()
      data.append('id', getters.getCurrentCraftItem.id)
      data.append('platform', rootGetters['common/getPlatform'])
      data.append('progress', progress)
      this.$axios.setToken(rootGetters.getToken, 'Bearer')
      const result = await this.$axios.$post(`${this.$axios.defaults.baseURL}/play-craft`, data)
      return result
    } catch (e) {
      throw e.response
    }
  },
  async sell ({ rootGetters, commit }, data) {
    try {
      this.$axios.setToken(rootGetters.getToken, 'Bearer')
      const result = await this.$axios.$post(`${this.$axios.defaults.baseURL}/sell-item`, data)
      if (result.success) {
        if (rootGetters['chest/getWinItem']) {
          commit('user/changeUserBalance', rootGetters['chest/getWinItem'][`${rootGetters['common/getPlatform']}Price`], { root: true })
        }
        commit('chest/setWinItem', null, { root: true })
        commit('user/setUser', result.data.user, { root: true })
      }
      return result
    } catch (e) {
      throw e.response
    }
  }
}

export const getters = {
  getWinLiveItems: state => state.winLiveItems,
  getCurrentCraftItem: state => state.currentCraftItem,
  getBtnState: state => state.btnDisabled,
  getTypes: state => state.types,
  getCraftItems: state => state.craftItems
}
