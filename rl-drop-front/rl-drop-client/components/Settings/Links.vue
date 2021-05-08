<template>
  <div class="form__inputs">
    <MyInput
      v-model="steamLink"
      :rightIcon="false"
      :leftIcon="true"
      name="link1"
      label="Epic Game Nickname PC"
      type="text"
      class="form__input"
    >
      <template slot="leftIcon">
        <LaptopIcon />
      </template>
    </MyInput>
    <MyInput
      v-model="xboxLink"
      :rightIcon="false"
      :leftIcon="true"
      name="link2"
      label="Nickname Xbox"
      type="text"
      class="form__input"
    >
      <template slot="leftIcon">
        <XboxIcon />
      </template>
    </MyInput>
    <MyInput
      v-model="ps4Link"
      :rightIcon="false"
      :leftIcon="true"
      name="link3"
      label="Nickname PS4"
      type="text"
      class="form__input"
    >
      <template slot="leftIcon">
        <PlaystationIcon />
      </template>
    </MyInput>
  </div>
</template>

<script>
import showNotification from '@/mixins/showNotification'
import { mapGetters } from 'vuex'
import { eventBus } from '@/plugins/event-bus'
import PlaystationIcon from 'vue-material-design-icons/SonyPlaystation.vue'
import XboxIcon from 'vue-material-design-icons/MicrosoftXbox.vue'
import LaptopIcon from 'vue-material-design-icons/Laptop.vue'
export default {
  components: {
    PlaystationIcon,
    XboxIcon,
    LaptopIcon
  },
  mixins: [showNotification],
  mounted () {
    this.ps4Link = this.getUser.ps4Link
    this.xboxLink = this.getUser.xboxLink
    this.steamLink = this.getUser.steamLink
    eventBus.$off('updateLinks')
    eventBus.$on('updateLinks', async () => {
      const data = {}
      data.steamLink = this.steamLink
      data.xboxLink = this.xboxLink
      data.ps4Link = this.ps4Link
      this.$store.commit('settings/toggleButtonState', true)
      try {
        const result = await this.$store.dispatch('user/updateLinks', data)
        if (result.success) {
          this.showNotification('Links changed successfully!', 'success')
        }
        console.log(result)
      } catch (e) {
        this.showNotification('Something went wrong!', 'danger')
        console.log(e)
      } finally {
        this.$store.commit('settings/toggleButtonState', false)
      }
    })
  },
  computed: {
    ...mapGetters({
      getUser: 'user/getUser'
    })
  },
  data () {
    return {
      steamLink: '',
      xboxLink: '',
      ps4Link: ''
    }
  }
}
</script>
