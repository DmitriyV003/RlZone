export const state = () => ({
  user: null,
  inventory: [],
  soldItems: [],
  withdrewItems: []
})

// noinspection SpellCheckingInspection
export const mutations = {
  setUser (state, user) {
    state.user = user
  },
  changeUser (state, data) {
    state.user.email = data.email
    state.user.name = data.name
    state.user.phoneNumber = data.phoneNumber
  },
  deleteUser (state) {
    state.user = null
  },
  changeUserBalance (state, balance) {
    state.user.balance += balance
    state.user.balance = Number(state.user.balance.toFixed(2))
  },
  beautifyBalance (state, id) {
    state.user.balance = Number(state.user.balance.toFixed(2))
  },
  setInventory (state, inventory) {
    state.inventory = inventory
  },
  deleteInventoryItemById (state, id) {
    state.inventory = state.inventory.filter(item => item.pivot.id !== id)
  },
  changeUserPhoto (state, photo) {
    state.user.photo = photo
  },
  setLinks (state, data) {
    state.user.steamLink = data.steamLink
    state.user.xboxLink = data.xboxLink
    state.user.ps4Link = data.ps4Link
  },
  setSoldItems (state, items) {
    state.soldItems = items
  },
  setWithdrewItems (state, items) {
    state.withdrewItems = items
  }
}

export const actions = {
  async getUser ({ commit, dispatch, getters }, token) {
    try {
      this.$axios.setToken(token, 'Bearer')
      const result = await this.$axios.$post(`${this.$axios.defaults.baseURL}/user`, { token })
      commit('setUser', result.data.user)
      // commit('2fa/set2faSecret', getters.getUser.passwordSecurity
      //   .google2fa_secret, { root: true })
      // dispatch('2fa/generateOneTimePassword', getters['2fa/get2faSecret'], { root: true })
      if (result.success) {
        commit('setUser', result.data.user)
        if (result.data.user.passwordSecurity.google2fa_enable) {
          commit('2fa/toggle2fa', result.data.user.passwordSecurity.google2fa_enable)
          commit('2fa/set2faImg', result.data.google2fa_url)
        }
      }
    } catch (e) {
      throw e.response
    }
  },
  async loadInventory ({ commit, rootGetters }) {
    try {
      this.$axios.setToken(rootGetters.getToken, 'Bearer')
      const result = await this.$axios.$get(`${this.$axios.defaults.baseURL}/inventory`)
      commit('setInventory', result.data.inventory || [])
      commit('setSoldItems', result.data.soldItems)
      commit('setWithdrewItems', result.data.withdrewItems)
      return result
    } catch (e) {
      throw e.response
    }
  },
  async changePhoto ({ commit, rootGetters }, data) {
    try {
      this.$axios.setToken(rootGetters.getToken, 'Bearer')
      const result = await this.$axios.$post(`${this.$axios.defaults.baseURL}/user/change-photo`, data)
      if (result.success) {
        commit('changeUserPhoto', result.data)
      }
      return result
    } catch (e) {
      throw e.response
    }
  },
  async loadStats ({ rootGetters }) {
    try {
      this.$axios.setToken(rootGetters.getToken, 'Bearer')
      return await this.$axios.$get(`${this.$axios.defaults.baseURL}/user/stats`)
    } catch (e) {
      throw e.response
    }
  },
  async updateLinks ({ rootGetters, commit }, data) {
    try {
      this.$axios.setToken(rootGetters.getToken, 'Bearer')
      const result = await this.$axios.$post(`${this.$axios.defaults.baseURL}/user/links`, data)
      commit('setLinks', data)
      return result
    } catch (e) {
      throw e.response
    }
  },
  async withdraw ({ rootGetters, commit }, payload) {
    try {
      this.$axios.setToken(rootGetters.getToken, 'Bearer')
      const data = new FormData()
      data.append('itemId', payload.id)
      data.append('pivotId', payload.pivotId)
      data.append('platform', payload.platform)
      const result = await this.$axios.$post(`${this.$axios.defaults.baseURL}/user/withdraw`, data)
      return result
    } catch (e) {
      throw e.response
    }
  }
}

export const getters = {
  getUser: state => state.user,
  getInventory: state => state.inventory,
  getSoldItems: state => state.soldItems,
  getWithdrewItems: state => state.withdrewItems
}
