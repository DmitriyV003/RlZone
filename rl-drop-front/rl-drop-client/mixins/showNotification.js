export default {
  methods: {
    showNotification (text, type) {
      this.$bvToast.toast(text, {
        title: this.$t('notification'),
        variant: type,
        solid: true
      })
    }
  }
}
