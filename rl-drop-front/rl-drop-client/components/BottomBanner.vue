<template>
  <div class="chance">
    <b-row>
      <img v-if="getWindowSize > 992" :src="image" alt="" class="chance__bg">
      <img v-else :src="mobileImage" alt="" class="chance__bg">
      <b-col xl="5" lg="5" md="8">
        <h2 class="chance__title">
          {{ title }}
        </h2>
        <p class="chance__text">
          {{ this[`text_${$i18n.locale}`] }}
        </p>
        <nuxt-link tag="button" :to="`/case/${caseId}`" class="btn btn_primary btn-arrow chance__btn">
          <span>Try It Now</span>
          <ArrowRightIcon class="btn__icon" />
        </nuxt-link>
      </b-col>
    </b-row>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import ArrowRightIcon from 'vue-material-design-icons/ArrowRight.vue'

export default {
  name: 'BottomIndexBanner',
  components: {
    ArrowRightIcon
  },
  computed: {
    ...mapGetters({
      getWindowSize: 'common/getWindowSize'
    })
  },
  props: {
    title: {
      type: null
    },
    image: {
      type: null
    },
    mobileImage: {
      type: null
    },
    caseId: {
      type: null
    },
    text_ru: {
      type: null
    },
    text_en: {
      type: null
    }
  }
}
</script>

<style lang="sass">
@import '@/theme/_mix.sass'
.chance
  border-radius: 12px
  box-shadow: 0 8px 8px -4px rgba(20, 16, 41, 0.24), 0 2px 4px -1px rgba(20, 16, 41, 0.24), 0 0 1px 0 rgba(20, 16, 41, 0.4)
  background-color: #27273e
  padding: 40px
  position: relative
  min-height: 352px
  &__bg
    position: absolute
    right: 0
    bottom: 0
    border-radius: 12px
    width: 100%
    height: 100%
    +lg
      width: initial
  &__title
    margin-bottom: 32px
  &__text
    color: rgba(224, 224, 255, 0.6)
    font-size: 18px
    font-weight: 500
    margin-bottom: 24px
  &__btn
    +sm
      width: 100%
      justify-content: center
</style>
