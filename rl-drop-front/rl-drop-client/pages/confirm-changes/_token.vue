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
            {{ $t('changesSaved') }}
          </h1>
          <p class="regForm__text">
            <nuxt-link to="/dashboard" class="regForm__emp" tag="span">
              {{ $t('continue') }}
            </nuxt-link>
            or
            <nuxt-link to="/sign-in" class="regForm__emp" tag="span">
              {{ $t('signIn') }}
            </nuxt-link>
          </p>
        </b-col>
      </b-row>
    </b-container>
  </div>
</template>

<script>/* eslint-disable */
export default {
  layout: 'register',
  middleware: ['commit-changes']
}
</script>

<style lang="sass"></style>
