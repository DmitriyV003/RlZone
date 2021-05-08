import Cookies from 'js-cookie'
import Cookie from 'cookie'

export const state = () => ({
  windowSize: 0,
  platform: 'pc',
  online: 0,
  stats: {
    cases: 0,
    crafts: 0,
    users: 0
  }
})

export const mutations = {
  setWindowSize (state, windowsize) {
    state.windowSize = windowsize
  },
  setPlatform (state, platform) {
    state.platform = platform
  },
  setOnline (state, online) {
    state.online = online
  },
  setStats (state, stats) {
    state.stats = stats
  }
}

export const actions = {
  setCookiePlatform (ctx, platform) {
    Cookies.set('platform', platform)
  },
  initCookie ({ commit, dispatch }) {
    const cookieStr = process.browser
      ? document.cookie
      : this.app.context.req.headers.cookie

    const cookies = Cookie.parse(cookieStr || '') || {}
    const platform = cookies.platform || 'pc'
    dispatch('setCookiePlatform', platform)
    commit('setPlatform', platform)
  },
  SOCKET_online ({ commit }, data) {
    commit('setOnline', data)
  },
  async getIndexStats ({ commit }) {
    try {
      const result = await this.$axios.$get(`${this.$axios.defaults.baseURL}/stats`)
      commit('setStats', result.data)
    } catch (e) {
      throw e.response
    }
  }
}

export const getters = {
  getWindowSize: state => state.windowSize,
  getPlatform: state => state.platform,
  getOnline: state => state.online,
  getStats: state => state.stats
}
