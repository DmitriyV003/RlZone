<template>
  <div class="registerPage">
    <img src="/images/sign-up-bg.png" alt="" class="registerPage__bg">
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
            Sign Up
          </h1>
          <form @submit.prevent="register" action="" class="regForm">
            <MyInput
              v-model.trim="$v.userName.$model"
              :rightIcon="false"
              :leftIcon="false"
              name="Name"
              label="User name"
              type="text"
              class="regForm__input"
              :error="$v.userName.$error"
            >
              <template slot="error">
                <span v-if="!$v.userName.required && $v.userName.$error">Name required</span>
              </template>
            </MyInput>
            <MyInput
              v-model.trim="$v.email.$model"
              :rightIcon="false"
              :leftIcon="false"
              name="email"
              label="Email"
              type="text"
              class="regForm__input"
              :error="$v.email.$error"
            >
              <template slot="error">
                <span v-if="!$v.email.required && $v.email.$error">Email required</span>
                <span v-if="!$v.email.email && $v.email.$error">Field email should be a valid email</span>
              </template>
            </MyInput>
            <MyInput
              v-model.trim="$v.password.$model"
              :rightIcon="false"
              :leftIcon="false"
              name="password"
              label="Password"
              type="password"
              class="regForm__input"
              :error="$v.password.$error"
            >
              <template slot="error">
                <span v-if="!$v.password.required && $v.password.$error">Password required</span>
                <span v-if="!$v.password.minLength && $v.password.$error">Password min length is 6</span>
              </template>
            </MyInput>
            <MyInput
              v-model.trim="$v.confirmPassword.$model"
              :rightIcon="false"
              :leftIcon="false"
              name="confirmPassword"
              label="Confirm password"
              type="password"
              class="regForm__input"
              :error="$v.confirmPassword.$error"
            >
              <template slot="error">
                <span v-if="!$v.confirmPassword.required && $v.confirmPassword.$error">Password is required</span>
                <span v-if="!$v.confirmPassword.sameAsPassword && $v.confirmPassword.$error">Passwords must natch</span>
              </template>
            </MyInput>

            <div class="checkbox regForm__checkbox">
              <input id="privacy" v-model="$v.acceptPolicy.$model" type="checkbox" name="privacy" class="checkbox__input">
              <label for="privacy" class="checkbox__label">
                <span>
                  By checking this checkbox I confirm that I agree to the <nuxt-link to="/privacy" tag="span" class="emp">Terms of Service, Privacy Policy </nuxt-link> and <nuxt-link to="/privacy" tag="span" class="emp">Legal</nuxt-link>.
                </span>
              </label>
            </div>

            <div class="regForm__bottom">
              <p class="regForm__text">
                Already a member? <nuxt-link to="/sign-in" class="regForm__emp" tag="span">
                  Sign In
                </nuxt-link>
              </p>
              <button
                type="submit"
                class="btn btn_primary"
                :class="disabled || $v.$invalid ? 'btn_primary_disabled' : ''"
                :disabled="disabled || $v.$invalid">
                Sign Up
              </button>
            </div>
          </form>
        </b-col>
      </b-row>
    </b-container>
  </div>
</template>
<!--TODO Make validation-->
<script>
import { required, email, minLength, sameAs } from 'vuelidate/lib/validators'
import showNotification from '@/mixins/showNotification'
export default {
  layout: 'register',
  data () {
    return {
      userName: '',
      email: '',
      password: '',
      confirmPassword: '',
      acceptPolicy: true,
      disabled: false
    }
  },
  mixins: [showNotification],
  validations: {
    userName: {
      required
    },
    email: {
      required,
      email
    },
    password: {
      required,
      minLength: minLength(6)
    },
    confirmPassword: {
      required,
      sameAsPassword: sameAs('password')
    },
    acceptPolicy: {
      required
    }
  },
  methods: {
    async register () {
      this.$v.$touch()
      if (!this.$v.$invalid) {
        try {
          this.disabled = true
          const data = new FormData()
          data.append('name', this.userName)
          data.append('email', this.email)
          data.append('password', this.password)
          data.append('confirm_password', this.confirmPassword)
          data.append('accept_policy', this.acceptPolicy)
          const result = await this.$store.dispatch('register', data)
          if (result.success) {
            this.userName = ''
            this.email = ''
            this.password = ''
            this.confirmPassword = ''
            this.acceptPolicy = false
            await this.$router.push('/sign-in')
          }
        } catch (e) {
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
    align-items: flex-start
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
    &_big
      font-size: 16px
      line-height: 28px
      margin-bottom: 16px
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
</style>
