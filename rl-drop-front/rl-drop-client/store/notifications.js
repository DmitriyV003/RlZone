export const state = () => ({
  notifications: []
})

export const mutations = {
  deleteNotification (state, id) {
    state.notifications = state.notifications.filter(item => item.id !== id)
  },
  addNotification (state, notification) {
    state.notifications.unshift(notification)
  },
  setNotifications (state, notifications) {
    state.notifications = notifications
  },
  deleteAllNotifications (state) {
    state.notifications = []
  }
}

export const actions = {
  async readNotification ({ rootGetters }, id) {
    try {
      this.$axios.setToken(rootGetters.getToken, 'Bearer')
      await this.$axios.$get(`${this.$axios.defaults.baseURL}/read-notification/${id}`)
    } catch (e) {
      throw e.response
    }
  },
  async readAll ({ commit, rootGetters }) {
    try {
      this.$axios.setToken(rootGetters.getToken, 'Bearer')
      const result = await this.$axios.get(`${this.$axios.defaults.baseURL}/notifications/read-all`)
      if (result.status === 204) {
        commit('deleteAllNotifications')
      }
    } catch (e) {
      throw e.response
    }
  }
}

export const getters = {
  getNotifications (state) {
    return state.notifications
  }
}
