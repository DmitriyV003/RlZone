export const state = () => ({
  faqs: []
})

export const mutations = {
  setFaqs (state, faqs) {
    state.faqs = faqs
  },
  createFaq (state, faq) {
    state.faqs.push(faq)
  }
}

export const actions = {
  async loadFaqs ({ commit }) {
    try {
      this.$axios.setHeader('X-localization', this.$i18n.locale)
      const result = await this.$axios.$get(`${this.$axios.defaults.baseURL}/faqs`)
      commit('setFaqs', result.data.faqs || [])
      return result.data
    } catch (e) {
      throw e.response
    }
  },
  async createFaq ({ commit }, data) {
    try {
      const result = await this.$axios.$post(`${this.$axios.defaults.baseURL}/admin/create-faq`, data)
      commit('createFaq', result.data)
      return result
    } catch (e) {
      throw e.response
    }
  }
}

export const getters = {
  getFaqs: state => state.faqs
}
