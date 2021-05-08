<template>
  <form method="POST" class="form">
    <div class="form__top">
      <div @click="back" class="form__back" tag="div">
        <ArrowL class="icon" />
        <span>Back</span>
      </div>
      <h4>Personal</h4>
      <button class="btn form__btn" type="submit">
        Done
      </button>
    </div>
    <div class="form__inputs">
      <div class="input form__input input_error">
        <label for="username" class="input__label">Username</label>
        <input id="username" type="text" name="username" class="input__input">
        <span class="input__error">Error message</span>
      </div>
      <div class="input form__input">
        <label for="username" class="input__label">Phone number</label>
        <input id="number" type="text" name="number" class="input__input">
      </div>
      <div class="input form__input input_w100">
        <label for="username" class="input__label">Email</label>
        <input id="email" type="text" name="email" class="input__input">
      </div>
      <div class="input form__input input_w100">
        <label for="username" class="input__label">Email</label>
        <input id="email" type="text" name="email" class="input__input input__input_withIcon">
        <SteamIcon class="input__icon" />
      </div>
    </div>
  </form>
</template>

<script>
import SteamIcon from 'vue-material-design-icons/Steam.vue'
import ArrowL from 'vue-material-design-icons/ChevronLeft.vue'
export default {
  components: {
    SteamIcon,
    ArrowL
  }
}
</script>
