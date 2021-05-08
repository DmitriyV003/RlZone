export const state = () => ({
  topIndexBanner: null,
  bottomIndexBanner: null
})

export const mutations = {
  setTopIndexBanner (state, banner) {
    state.topIndexBanner = banner
  },
  setBottomIndexBanner (state, banner) {
    state.bottomIndexBanner = banner
  }
}

export const getters = {
  getTopIndexBanner: state => state.topIndexBanner,
  getBottomIndexBanner: state => state.bottomIndexBanner
}
