<template>
  <div class="verification__form">
    <div class="form__inputs">
      <MyInput
        v-model="twoFaSecret"
        :rightIcon="false"
        :leftIcon="false"
        name="address"
        label="Address"
        type="text"
        class="form__input form__input_w100"
        :readOnly="true"
      />
      <MyInput
        v-model="twoFaCode"
        :rightIcon="false"
        :leftIcon="false"
        name="code"
        label="Code"
        type="text"
        class="form__input form__input_w100"
        :readOnly="true"
      />
    </div>
    <img v-if="verificationImg" :src="verificationImg" alt="" class="verification__img">
  </div>
</template>

<script>
export default {
  props: {
    verificationImg: String,
    twoFaSecret: String,
    twoFaCode: String
  }
}
</script>

<style lang="sass">
  .verification
    &__form
      display: flex
      align-items: flex-start
    &__img
      width: 168px
      height: 168px
      margin-left: 24px
</style>
