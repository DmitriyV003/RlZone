export const actions = {
  async sendEmailForPasswordReset ({ rootGetters, commit }, data) {
    try {
      return await this.$axios.$post(`${this.$axios.defaults.baseURL}/reset-password/send-link`, data)
    } catch (e) {
      throw e.response
    }
  },
  async sendPassword ({ rootGetters, commit }, data) {
    try {
      return await this.$axios.$post(`${this.$axios.defaults.baseURL}/reset-password/new-password`, data)
    } catch (e) {
      throw e.response
    }
  },
  async changePassword ({ rootGetters, commit }, data) {
    try {
      this.$axios.setToken(rootGetters.getToken, 'Bearer')
      return await this.$axios.$post(`${this.$axios.defaults.baseURL}/change-password`, data)
    } catch (e) {
      throw e.response
    }
  }
}
