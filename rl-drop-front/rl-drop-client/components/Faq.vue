<template>
  <b-col xl="6" lg="6">
    <div class="faqItem">
      <div @click="showText === true ? showText = false : showText = true" class="faqItem__block">
        <p class="faqItem__title">
          {{ title }}
        </p>
        <PlusIcon v-if="showText === false" class="faqItem__icon" />
        <MinusIcon v-if="showText === true" class="faqItem__icon" />
      </div>
      <div v-if="showText === true" class="faqItem__hide">
        <p>{{ text }}</p>
      </div>
    </div>
  </b-col>
</template>

<script>
import PlusIcon from 'vue-material-design-icons/Plus.vue'
import MinusIcon from 'vue-material-design-icons/Minus.vue'
export default {
  components: {
    PlusIcon,
    MinusIcon
  },
  props: {
    title: {
      type: null,
      required: true
    },
    text: {
      type: null,
      required: true
    }
  },
  data () {
    return {
      showText: false
    }
  }
}
</script>

<style lang="sass">
@import '@/theme/_mix.sass'
.faqItem
  padding: 24px
  border-radius: 12px
  box-shadow: 0 8px 8px -4px rgba(20, 16, 41, 0.24), 0 2px 4px -1px rgba(20, 16, 41, 0.24), 0 0 1px 0 rgba(20, 16, 41, 0.4)
  background-color: #27273e
  margin-bottom: 16px
  &__block
    cursor: pointer
    color: white
    display: flex
    align-items: flex-start
    justify-content: space-between
    margin: -24px -24px 0 -24px
    padding: 24px 24px 0 24px
  &__icon
    color: rgba(224, 224, 255, 0.4)
    font-size: 25px
    margin-left: 24px
  &__title
    font-size: 18px
    font-weight: 500
  &__hide
    margin-top: 24px
    color: rgba(224, 224, 255, 0.6)
    font-weight: 500
    span
      color: #00bbff
      cursor: pointer
</style>
