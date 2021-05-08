export default {
  methods: {
    privateRoomConnect () {
      window.Echo.private(`room.${this.getUser.id}`)
        .listen('CreateNotification', (e) => {
          // this.message = e.message
          console.log((e))
          this.$store.commit('notifications/addNotification', e.notification)
        })
    }
  }
}
