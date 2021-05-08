export default function ({ $axios, redirect, store }) {
  // $axios.interceptors.request.use(request => {
  //   if (store.getters['auth/isAuthenticated'] && !request.headers.common['Authorization']) {
  //     const token = store.getters['auth/token']
  //     request.headers.common['Authorization'] = `Bearer ${token}`
  //   }
  //
  //   return request
  // })
  // $axios.onError((error) => {
  //   if (error.response) {
  //     if (error.response.status === 401) {
  //       redirect('/sign-in?message=error')
  //     }
  //   }
  // })
}
