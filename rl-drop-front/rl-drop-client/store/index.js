import Cookie from 'cookie'
import Cookies from 'js-cookie'
import jwt from 'jsonwebtoken'

export const state = () => ({
  token: null,
  tokenExpireDate: '',
  lang: 'en',
  errors: null,
  disabledBtn: false
})

export const mutations = {
  setToken (state, token) {
    state.token = token
    state.tokenExpireDate = jwt.decode(token).exp
  },
  deleteToken (state) {
    state.token = null
  },
  deleteUser (state) {
    state.user = null
  },
  setErrors (state, errors) {
    state.errors = errors
  },
  deleteErrors (state, errors) {
    state.errors = null
  }
}

export const actions = {
  async nuxtServerInit ({ dispatch, getters, commit, rootGetters }) {
    try {
      dispatch('autoLogin')
      dispatch('common/initCookie')
      if (getters.getToken) {
        await dispatch('user/getUser', getters.getToken)
        commit('notifications/setNotifications', rootGetters['user/getUser'].notifications)
        commit('2fa/set2faSecret', rootGetters['user/getUser'].passwordSecurity.google2fa_secret)
        dispatch('2fa/generateOneTimePassword', rootGetters['2fa/get2faSecret'])
      }
    } catch (e) {
      throw e.response
    }
  },
  setToken ({ commit }, token) {
    this.$axios.setToken(token, 'Bearer')
    commit('setToken', token)
    Cookies.remove('token')
    Cookies.set('token', token, { expires: 1 })
  },
  async logOut ({ commit, getters }) {
    try {
      this.$axios.setToken(getters.getToken, 'Bearer')
      const result = await this.$axios.$post(`${this.$axios.defaults.baseURL}/logout`)
      commit('deleteToken')
      commit('2fa/delete2faImg')
      commit('user/deleteUser')
      commit('user/setInventory', [])
      commit('2fa/toggle2fa', null)
      commit('2fa/set2faCode', null)
      commit('2fa/set2faSecret', null)
      commit('notifications/setNotifications', [])
      Cookies.remove('token')
      await this.$router.replace({ path: '/' })
      return result
    } catch (e) {
      throw e.response
    }
  },
  autoLogin ({ dispatch, commit }) {
    const cookieStr = process.browser
      ? document.cookie
      : this.app.context.req.headers.cookie

    const cookies = Cookie.parse(cookieStr || '') || {}
    const token = cookies.token
    if (isJWTValid(token)) {
      dispatch('setToken', token)
    } else {
      // dispatch('logOut')
      commit('deleteToken')
    }
  },
  async register ({ dispatch }, data) {
    try {
      return await this.$axios.$post(`${this.$axios.defaults.baseURL}/register`, data)
    } catch (e) {
      throw e.response
    }
  },
  async preLogin ({ dispatch, commit }, formData) {
    try {
      const result = await this.$axios.$post(`${this.$axios.defaults.baseURL}/login`, formData)
      if (result.data.success && result.data.google2faEnable) {
        commit('2fa/toggle2fa', true)
      } else if (result.success && result.data.user.passwordSecurity.google2faEnable) {
        commit('2fa/toggle2fa', true)
      } else if (result.success && !result.data.user.passwordSecurity.google2faEnable) {
        dispatch('setToken', result.data.access_token)
        commit('user/setUser', result.data.user)
      }
      return result
    } catch (e) {
      throw e.response
    }
  },
  async loginWith2fa ({ dispatch, commit, getters }, formData) {
    try {
      const result = await dispatch('2fa/verify2fa', formData)
      if (result.success && result.data.loggedIn) {
        dispatch('setToken', result.data.access_token)
        commit('user/setUser', result.data.user)
        commit('2fa/set2faImg', result.data.twofaImg)
        commit('2fa/set2faSecret', result.data.user.passwordSecurity.google2fa_secret)
        dispatch('2fa/generateOneTimePassword', getters['2fa/get2faSecret'])
      }
      return result
    } catch (e) {
      throw e.response
    }
  }
}

export const getters = {
  getToken: state => state.token,
  getExpireDate: state => state.tokenExpireDate,
  getErrors: state => state.errors,
  getBtnState: state => state.disabledBtn
}

function isJWTValid (token) {
  if (!token) {
    return false
  }

  const jwtData = jwt.decode(token) || {}
  const expires = jwtData.exp || 0

  return (new Date().getTime() / 1000) < expires
}
