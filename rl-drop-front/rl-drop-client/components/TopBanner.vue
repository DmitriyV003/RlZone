<template>
  <div class="ice">
    <img v-if="getWindowSize > 576" :src="image" alt="" class="ice__bg">
    <img v-else :src="mobileImage" alt="" class="ice__bg">
    <div class="ice__text">
      <h2 class="ice__title">
        {{ title }}
      </h2>
      <Timer
        :starttime="starttime"
        :endtime="endtime"
        trans="{
                &quot;day&quot;:&quot;Day&quot;,
                &quot;hours&quot;:&quot;Hrs&quot;,
                &quot;minutes&quot;:&quot;Min&quot;,
                &quot;seconds&quot;:&quot;Sec&quot;,
                &quot;expired&quot;:&quot;Event has been expired.&quot;,
                &quot;running&quot;:&quot;Till the end of event.&quot;,
                &quot;upcoming&quot;:&quot;Till start of event.&quot;,
                &quot;status&quot;: {
                   &quot;expired&quot;:&quot;Expired&quot;,
                   &quot;running&quot;:&quot;Running&quot;,
                   &quot;upcoming&quot;:&quot;Future&quot;
                  }}"
      />
    </div>
    <div class="ice__btn">
      <button @click.prevent="scrollTo" class="btn btn_primary btn-arrow">
        <span>{{ $t('play') }}</span>
        <ArrowRightIcon class="btn__icon" />
      </button>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import showNotification from '@/mixins/showNotification'
import ArrowRightIcon from 'vue-material-design-icons/ArrowRight.vue'
import Timer from '../components/Timer'

export default {
  name: 'TopIndexBanner',
  components: {
    Timer,
    ArrowRightIcon
  },
  mixins: [showNotification],
  methods: {
    scrollTo () {
      try {
        document.getElementById(this.caseCategory).scrollIntoView({ behavior: 'smooth' })
      } catch (e) {
        this.showNotification(this.$t('smtWrong'), 'danger')
      }
    }
  },
  props: {
    title: {
      type: String,
      required: true
    },
    image: {
      type: null
    },
    mobileImage: {
      type: null
    },
    starttime: {
      type: null
    },
    endtime: {
      type: null
    },
    caseCategory: {
      type: null
    }
  },
  computed: {
    ...mapGetters({
      getWindowSize: 'common/getWindowSize'
    })
  }
}
</script>

<style lang="sass">
@import '@/theme/_mix.sass'
.ice
  border-radius: 12px
  box-shadow: 0 8px 8px -4px rgba(20, 16, 41, 0.24), 0 2px 4px -1px rgba(20, 16, 41, 0.24), 0 0 1px 0 rgba(20, 16, 41, 0.4)
  background-color: #27273e
  padding: 40px
  display: flex
  align-items: flex-start
  justify-content: space-between
  position: relative
  min-height: 228px
  +md
    flex-direction: column
  &__title
    margin-bottom: 40px
  &__bg
    position: absolute
    right: -30px
    bottom: -30px
    user-select: none
    +lg
      width: 100%
      bottom: -22px
    +md
      bottom: -17px
      right: 0
      border-radius: 0 0 12px 12px
  &__btn
    position: relative
    z-index: 5
    +md
      width: 100%
      margin-top: 66px
      .btn
        width: 100%
        justify-content: center
  &__text
    position: relative
    z-index: 5
    +md
      width: 100%
  &__timer
    display: flex
    align-items: flex-start
    user-select: none
    +md
      justify-content: center
      width: 100%
    &-item
      display: flex
      flex-direction: column
      align-items: center
      &:not(:last-child)
        margin-right: 8px
      .num
        width: 48px
        height: 48px
        letter-spacing: -1px
        margin-bottom: 8px
        display: flex
        align-items: center
        justify-content: center
        font-size: 24px
        font-weight: bold
        color: white
        border-radius: 4px
        text-transform: uppercase
        background-color: rgba(224, 224, 255, 0.02)
      .day
        text-align: center
        font-size: 13px
        line-height: 16px
        text-transform: uppercase
        color: rgba(224, 224, 255, 0.6)
        letter-spacing: 1px
        font-weight: 600
      .expired
        font-size: 24px
        text-transform: uppercase
        font-weight: 600
        color: rgba(224, 224, 255, 0.6)
</style>
