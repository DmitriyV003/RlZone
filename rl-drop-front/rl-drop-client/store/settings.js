export const state = () => ({
  isButtonDisabled: false
})

export const mutations = {
  toggleButtonState (state, payload) {
    state.isButtonDisabled = payload
  }
}

export const actions = {
  async updateSettings ({ rootGetters, commit }, data) {
    try {
      const userId = rootGetters['user/getUser'].id
      this.$axios.setToken(rootGetters.getToken, 'Bearer')
      const result = await this.$axios.$post(`${this.$axios.defaults.baseURL}/user/${userId}/update`, data)
      if (result.success) {
        const newUser = {
          name: data.get('name'),
          email: data.get('email'),
          phoneNumber: data.get('phone_number')
        }
        commit('user/changeUser', newUser, { root: true })
      }
      return result
    } catch (e) {
      throw e.response
    }
  }
}

export const getters = {
  getButtonState: state => state.isButtonDisabled
}
