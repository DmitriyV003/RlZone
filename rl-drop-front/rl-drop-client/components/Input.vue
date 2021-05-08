<template>
  <div :class="{'input_error': error}" class="input">
    <div class="input__icon">
      <slot name="leftIcon" class="" />
    </div>
    <slot name="label">
      <label :for="name" class="input__label">{{ label }}</label>
    </slot>
    <input
      :id="name"
      :value="value"
      @input="$emit('input', $event.target.value)"
      :placeholder="label"
      :type="type"
      :name="name"
      :readonly="readOnly"
      :class="{
        'input__input_withIconR': detectIcon.type === 'right',
        'input__input_withIconL': detectIcon.type === 'left',
        'input__input_withIconR input__input_withIconL': detectIcon.type === 'both',
        'input__input_readonly': readOnly
      }"
      class="input__input"
    >
    <span class="input__error"><slot name="error"></slot></span>
    <div class="input__icon_r input__icon">
      <slot name="rightIcon" class="" />
    </div>
  </div>
</template>

<script>
export default {
  props: {
    value: {
      type: null,
      required: true
    },
    label: {
      type: String,
      required: true
    },
    type: {
      type: String,
      required: false,
      default: 'text'
    },
    name: {
      type: String,
      required: true
    },
    error: {
      type: Boolean,
      required: false,
      default: false
    },
    rightIcon: {
      type: Boolean,
      required: false,
      default: false
    },
    leftIcon: {
      type: Boolean,
      required: false,
      default: false
    },
    readOnly: {
      type: Boolean,
      required: false,
      default: false
    }
  },
  computed: {
    detectIcon () {
      if (this.rightIcon && !this.leftIcon) {
        return {
          hasIcon: true,
          type: 'right'
        }
      } else if (this.leftIcon && !this.rightIcon) {
        return {
          hasIcon: true,
          type: 'left'
        }
      } else if (this.rightIcon && this.leftIcon) {
        return {
          hasIcon: true,
          type: 'both'
        }
      }
      return {
        hasIcon: false
      }
    },
    hasError () {
      if (this.error) {
        return true
      }
      return false
    }
  }
}
</script>

<style lang="sass">
.input
  display: flex
  flex-direction: column
  align-items: flex-start
  position: relative
  &_w100
    width: 100%
  &_error
    .input__input
      border: solid 2px #ff00aa
  &__icon
    position: absolute
    left: 8px
    bottom: 0
    width: 25px
    height: 25px
    transform: translateY(calc(-100% + 10px))
    color: rgba(224, 224, 255, 0.6)
    font-size: 25px
    &_r
      right: 8px
      left: initial
      cursor: pointer
  &__label
    padding: 0 12px
    font-size: 13px
    line-height: 16px
    color: rgba(224, 224, 255, 0.4)
    margin-bottom: 8px
    font-weight: 500
  &__input
    padding: 10px 16px
    border-radius: 4px
    background: rgba(224, 224, 255, 0.02)
    color: white
    border: 2px solid transparent
    font-size: 16px
    line-height: 25px
    font-weight: 500
    width: 100%

    &_withIconL
      padding-left: 38px
    &_withIconR
      padding-right: 38px
    &:hover
      background: rgba(224, 224, 255, 0.04)
    &::placeholder
      color: rgba(224, 224, 255, 0.6)
    &:focus
      border: solid 2px rgba(224, 224, 255, 0.06)
    &_readonly
      &:focus
        border: 2px solid transparent !important
        cursor: initial !important
  &__error
    font-size: 13px
    line-height: 16px
    color: #ff00aa
    font-weight: 500
    position: absolute
    bottom: 0
    transform: translateY(calc(100% + 6px))
    left: 12px
</style>
