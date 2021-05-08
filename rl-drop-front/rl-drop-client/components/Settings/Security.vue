<template>
  <div class="form__inputs">
    <MyInput
      v-model.trim="$v.currentPassword.$model"
      :rightIcon="false"
      :leftIcon="false"
      :error="Boolean(errorMessages.currentPassword) || $v.currentPassword.$error"
      name="currentPassword"
      label="Current Password"
      type="password"
      class="form__input"
    >
      <template slot="error">
        <span v-if="errorMessages.currentPassword">{{ errorMessages.currentPassword }}</span>
        <span v-if="!$v.currentPassword.required && $v.currentPassword.$error">Password required</span>
      </template>
    </MyInput>
    <MyInput
      v-model.trim="$v.newPassword.$model"
      :rightIcon="false"
      :leftIcon="false"
      :error="Boolean(errorMessages.newPassword) || $v.newPassword.$error"
      name="newPassword"
      label="New Password"
      type="password"
      class="form__input"
    >
      <template slot="error">
        <span v-if="errorMessages.newPassword">{{ errorMessages.newPassword[0] }}</span>
        <span v-if="!$v.newPassword.required && $v.newPassword.$error">New Password required</span>
        <span v-if="!$v.newPassword.minLength && $v.newPassword.$error">Password min length is {{ $v.newPassword.$params.minLength.min }}</span>
      </template>
    </MyInput>
    <MyInput
      v-model.trim="$v.confirmPassword.$model"
      :rightIcon="false"
      :leftIcon="false"
      :error="Boolean(errorMessages.confirmPassword) || $v.confirmPassword.$error"
      name="confirmPassword"
      label="Confirm Password"
      type="password"
      class="form__input form__input_w100"
    >
      <template slot="error">
        <span v-if="errorMessages.confirmPassword">{{ errorMessages.confirmPassword[0] }}</span>
        <span v-if="!$v.confirmPassword.required && $v.confirmPassword.$error">Confirm your password</span>
      </template>
    </MyInput>
  </div>
</template>

<script>import { required, minLength } from 'vuelidate/lib/validators'
import showNotification from '@/mixins/showNotification'
import { eventBus } from '~/plugins/event-bus'
export default {
  mixins: [showNotification],
  data () {
    return {
      currentPassword: '',
      newPassword: '',
      confirmPassword: '',
      errorMessages: {
        currentPassword: '',
        newPassword: '',
        confirmPassword: ''
      }
    }
  },
  validations: {
    currentPassword: {
      required
    },
    newPassword: {
      required,
      minLength: minLength(6)
    },
    confirmPassword: {
      required
    }
  },
  created () {
    eventBus.$on('changePassword', async () => {
      this.$store.commit('settings/toggleButtonState', true)
      this.$v.$touch()
      if (!this.$v.$invalid) {
        const formData = new FormData()
        formData.append('currentPassword', this.currentPassword)
        formData.append('newPassword', this.newPassword)
        formData.append('confirmPassword', this.confirmPassword)

        try {
          const result = await this.$store.dispatch('password/changePassword', formData)
          if (result.success) {
            this.showNotification(this.$t('passwordLink'), 'primary')
            this.errorMessages.currentPassword = null
            this.errorMessages.newPassword = null
            this.errorMessages.confirmPassword = null
            this.currentPassword = ''
            this.newPassword = ''
            this.confirmPassword = ''
            this.$v.$reset()
          } else {
            this.$bvToast.toast('Something goes wrong!', {
              title: `Notification`,
              variant: 'danger',
              solid: true
            })
          }
        } catch (e) {
          if (e.status === 404) {
            this.errorMessages.currentPassword = 'Invalid password'
          } else if (e.status === 400) {
            this.errorMessages.currentPassword = e.data.error.messages.currentPassword || ''
            this.errorMessages.newPassword = e.data.error.messages.newPassword || ''
            this.errorMessages.confirmPassword = e.data.error.messages.confirmPassword || ''
          }
        } finally {
          this.$store.commit('settings/toggleButtonState', false)
        }
      }
    })
  }
}
</script>
