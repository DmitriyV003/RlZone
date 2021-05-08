<template>
  <div class="registerPage registerPage_signIn">
    <img src="/images/password-bg.png" alt="" class="registerPage__bg">
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
            Password Reset
          </h1>
          <p class="regForm__text regForm__text_big">
            Enter new password.
          </p>
          <form @submit.prevent="onSubmit" class="regForm">
            <MyInput
              v-model.trim="$v.password.$model"
              :rightIcon="false"
              :leftIcon="false"
              :error="Boolean(errorMessage) || $v.password.$error"
              type="password"
              name="password"
              label="Password"
              class="regForm__input"
            >
              <template slot="error">
                <span v-if="errorMessage">{{ errorMessage }}</span>
                <span v-if="!$v.password.required && $v.password.$error">Password required</span>
                <span v-if="!$v.password.minLength && $v.password.$error">Password should contains min {{ $v.password.$params.minLength.min }} symbols</span>
              </template>
            </MyInput>

            <MyInput
              v-model.trim="$v.confirmPassword.$model"
              :rightIcon="false"
              :leftIcon="false"
              :error="Boolean(errorMessage) || $v.confirmPassword.$error"
              type="password"
              name="confirmPassword"
              label="Confirm Password"
              class="regForm__input"
            >
              <template slot="error">
                <span v-if="errorMessage">{{ errorMessage }}</span>
                <span v-if="!$v.confirmPassword.required && $v.confirmPassword.$error">Confirm Password required</span>
                <span v-if="!$v.confirmPassword.sameAsPassword && $v.confirmPassword.$error">Passwords should match</span>
              </template>
            </MyInput>

            <div class="regForm__bottom">
              <p class="regForm__text">
                <nuxt-link to="/sign-in" class="regForm__emp" tag="span">
                  Sign In Now
                </nuxt-link>
              </p>
              <button :class="disabled || $v.$invalid ? 'btn_primary_disabled' : ''" :disabled="disabled || $v.$invalid" class="btn btn_primary" type="submit">
                Reset
              </button>
            </div>
          </form>
        </b-col>
      </b-row>
    </b-container>
  </div>
</template>

<script>/* eslint-disable */
import { sameAs, required, minLength } from 'vuelidate/lib/validators'
import showNotification from '@/mixins/showNotification'

export default {
    layout: 'register',
    data () {
      return {
        password: '',
        confirmPassword: '',
        errorMessage: null,
        disabled: false
      }
    },
  mixins: [showNotification],
  validations: {
    confirmPassword: {
      required,
      sameAsPassword: sameAs('password')
    },
    password: {
      required,
      minLength: minLength(6)
    }
  },
    methods: {
      async onSubmit () {
        if (!this.$v.$invalid) {
          try {
            this.disabled = true
            const formData = new FormData()
            formData.append('password', this.password)
            formData.append('confirmPassword', this.confirmPassword)
            formData.append('token', this.$route.params.token)
            const result = await this.$store.dispatch('password/sendPassword', formData)
            if (result.success) {
              this.errorMessage = null
              await this.$router.push('/sign-in')
            }
          } catch (e) {
            this.errorMessage = e.data.message || e.data.error.messages[0]
            this.showNotification(this.showNotification(this.$t('smtWrong'), 'danger'))
          }
        }
      }
    }
  }
</script>

<style lang="sass"></style>
