import { authenticator } from 'otplib'
export const state = () => ({
  img2fa: '',
  is2faEnable: false,
  twoFaSecret: null,
  twoFaCode: null
})

export const mutations = {
  set2faImg (state, img) {
    state.img2fa = img
  },
  delete2faImg (state) {
    state.img2fa = null
  },
  toggle2fa (state, toggler) {
    state.is2faEnable = toggler
  },
  set2faSecret (state, secret) {
    state.twoFaSecret = secret
  },
  set2faCode (state, code) {
    state.twoFaCode = code
  }
}

export const actions = {
  async generateSecret2fa ({ rootGetters, commit, dispatch }) {
    this.$axios.setToken(rootGetters.getToken, 'Bearer')
    try {
      const result = await this.$axios.$post(`${this.$axios.defaults.baseURL}/enable2fa`)
      if (result.success) {
        commit('set2faImg', result.data.google2fa_url)
        commit('set2faSecret', result.data.user.password_security.google2fa_secret)
        dispatch('generateOneTimePassword')
        commit('toggle2fa', true)
      }
      return result
    } catch (e) {
      throw e.response
    }
  },
  async verify2fa (ctx, formData) {
    try {
      return await this.$axios.$post(`${this.$axios.defaults.baseURL}/login`, formData)
    } catch (e) {
      throw e.response
    }
  },
  async disable2fa ({ rootGetters, commit }) {
    try {
      this.$axios.setToken(rootGetters.getToken, 'Bearer')
      const result = await this.$axios.$post(`${this.$axios.defaults.baseURL}/disable2fa/${rootGetters['user/getUser'].id}`)
      if (result.success) {
        commit('toggle2fa', false)
        commit('set2faSecret', '')
        commit('set2faCode', '')
        commit('set2faImg', '')
      }
      return result
    } catch (e) {
      throw e.response
    }
  },
  generateOneTimePassword ({ getters, commit }) {
    if (getters.get2faSecret) {
      const code = authenticator.generate(getters.get2faSecret)
      commit('set2faCode', code)
    }
  }
}

export const getters = {
  get2faImg: state => state.img2fa,
  get2fa: state => state.is2faEnable,
  get2faSecret: state => state.twoFaSecret,
  get2faCode: state => state.twoFaCode
}
