<template>
  <div class="registerPage registerPage_signIn">
    <img src="/images/sign-in-bg.png" alt="" class="registerPage__bg">
    <b-container>
      <b-row>
        <b-col
          xl="6"
          offset-xl="6"
          lg="6"
          offset-lg="6"
          md="7"
          offset-md="5"
        >
          <h1 class="registerPage__title">
            Sign In
          </h1>
          <form @submit.prevent="login" class="regForm">
            <MyInput
              v-model.trim="$v.email.$model"
              :rightIcon="false"
              :leftIcon="false"
              @input="errorMessages.invalidCredentials = false"
              :error="$v.email.$error || errorMessages.invalidCredentials"
              name="Email"
              label="Email"
              type="text"
              class="regForm__input"
            >
              <template slot="error">
                <span v-if="errorMessages.email[0] || errorMessages.invalidCredentials">{{ errorMessages.email[0] || 'Invalid Credentials' }}</span>
                <span v-if="!$v.email.required && $v.email.$error">Email required</span>
                <span v-if="!$v.email.email && $v.email.$error">Email should be valid</span>
              </template>
            </MyInput>
            <MyInput
              v-model.trim="$v.password.$model"
              :rightIcon="true"
              :leftIcon="false"
              :type="showPassword ? 'text' : 'password'"
              @input="errorMessages.invalidCredentials = false"
              :error="$v.password.$error || errorMessages.invalidCredentials"
              name="password"
              label="Password"
              class="regForm__input"
            >
              <template slot="error">
                <span v-if="errorMessages.password[0] || errorMessages.invalidCredentials">{{ errorMessages.password[0] || 'Invalid Credentials' }}</span>
                <span v-if="!$v.password.required && $v.password.$error">Password required</span>
              </template>
              <template slot="rightIcon">
                <EyeIcon v-if="!showPassword" @click="showPassword = true" />
                <EyeOffIcon v-if="showPassword" @click="showPassword = false" />
              </template>
              <template slot="label">
                <div class="input__label input__custom-label">
                  <label for="password">Password</label>
                  <nuxt-link tag="span" class="emp" to="/password-recovery">
                    Forgot password?
                  </nuxt-link>
                </div>
              </template>
            </MyInput>

            <MyInput
              v-model.trim="$v.twoFA.$model"
              :rightIcon="false"
              :leftIcon="false"
              v-if="get2fa"
              :error="errorMessages.invalidCode"
              name="TwoFA"
              label="2FA Code"
              type="text"
              class="regForm__input"
            >
              <template slot="error">
                <span v-if="errorMessages.invalidCode">{{ 'Invalid code! Please try again.' }}</span>
              </template>
            </MyInput>

            <div class="regForm__bottom">
              <p class="regForm__text">
                Not a member? <nuxt-link to="/sign-up" class="regForm__emp" tag="span">
                  Sign Up Now
                </nuxt-link>
              </p>
              <button
                :class="disabled || $v.$invalid ? 'btn_primary_disabled' : ''"
                :disabled="disabled || $v.$invalid"
                class="btn btn_primary"
                type="submit"
              >
                Sign In
              </button>
            </div>
          </form>
        </b-col>
      </b-row>
    </b-container>
  </div>
</template>

<script>
import { required, email } from 'vuelidate/lib/validators'
import socket from '@/mixins/socket'
import showNotification from '@/mixins/showNotification'
import EyeIcon from 'vue-material-design-icons/EyeOutline.vue'
import EyeOffIcon from 'vue-material-design-icons/EyeOffOutline.vue'
import { mapGetters } from 'vuex'
export default {
  layout: 'register',
  components: {
    EyeIcon,
    EyeOffIcon
  },
  mixins: [showNotification, socket],
  computed: {
    ...mapGetters({
      get2fa: '2fa/get2fa',
      getUser: 'user/getUser'
    })
  },
  data () {
    return {
      showPassword: false,
      email: '',
      twoFA: '',
      password: '',
      disabled: false,
      errorMessages: {
        email: '',
        password: '',
        invalidCredentials: null,
        invalidCode: false
      }
    }
  },
  validations: {
    email: {
      required,
      email
    },
    password: {
      required
    },
    twoFA: {}
  },
  mounted () {
    /* eslint-disable */
    // this.$echo.private(`room.1`).listen('CustomNotification', (payload) => {
    //   console.log(payload)
    // })
    // console.log(this.$echo)
  },
  methods: {
    async login () {

      if (!this.$v.$invalid) {
        try {
          this.disabled = true
          const data = new FormData()
          data.append('email', this.email)
          data.append('password', this.password)
          if (this.twoFA) {
            data.append('one_time_password', this.twoFA)
          }
          let result = {}
          if (!this.get2fa) {
            result = await this.$store.dispatch('preLogin', data)
          } else {
            try {
              result = await this.$store.dispatch('loginWith2fa', data)
              if (!result.success) {
                this.errorMessages.invalidCode = true
              } else {
                this.errorMessages.invalidCode = true
              }
            } catch (e) {
              this.showNotification('Invalid code', 'danger')
            }
          }
          if (result.data.loggedIn && this.getUser) {
            this.privateRoomConnect()
            await this.$router.replace({ path: '/' })
            this.errorMessages.invalidCode = false
          }
        } catch (e) {
          if (e.status === 401 || e.status === 400) {
            this.errorMessages.invalidCode = false
          } else if (e.data.invalidCode) {
            this.errorMessages.invalidCode = true
          } else {
            this.errorMessages.email = e.data.error.messages.email
            this.errorMessages.password = e.data.error.messages.password
          }
          for (const err in e.data.errors) {
            this.showNotification(e.data.errors[err][0], 'danger')
          }
          for (const err in e.data.error) {
            this.showNotification(e.data.error[err][0], 'danger')
          }
        } finally {
          this.disabled = false
        }
      }
    }
  }
}
</script>

<style lang="sass">
@import '@/theme/_mix.sass'
.registerPage
  min-height: 100vh
  position: relative
  display: flex
  align-items: center
  padding: 124px 0
  +md
    padding: 48px 0
  &__bg
    position: absolute
    left: 0
    top: 0
    height: 100%
    +md
      display: none
  &__title
    margin-bottom: 32px
.regForm
  &__input
    &:not(:last-child)
      margin-bottom: 30px
  &__checkbox
    margin-top: 30px
  &__bottom
    display: flex
    align-items: center
    justify-content: space-between
    margin-top: 24px
  &__emp
    color: #00bbff
    cursor: pointer
  &__text
    font-size: 14px
    color: rgba(224, 224, 255, 0.6)
    font-weight: 500
  .btn
    flex-shrink: 0
    +md
      margin-left: 40px
.checkbox
  font-weight: 500
  font-size: 14px
  color: rgba(224, 224, 255, 0.6)
  .emp
    color: #00bbff
    cursor: pointer
  &__input
    display: none
    &:checked
      & + .checkbox__label::before
        background-color: #00bbff
        background-image: url('/images/check.svg')
        background-repeat: no-repeat
        background-position: center center
  &__label
    display: flex
    align-items: flex-start
    cursor: pointer
    user-select: none
    margin: 0
    &:hover
      &::before
        background: rgba(224, 224, 255, 0.12)
    &::before
      width: 24px
      height: 24px
      flex-shrink: 0
      content: ''
      margin-right: 16px
      display: block
      background: rgba(224, 224, 255, 0.04)
      border-radius: 4px
.input
  &__custom
    &-label
      display: flex
      align-items: center
      width: 100%
      justify-content: space-between
      .emp
        color: #00bbff
        cursor: pointer
</style>
