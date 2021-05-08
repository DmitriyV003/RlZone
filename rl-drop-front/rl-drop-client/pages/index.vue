<template>
  <div>
    <!-- Timer  adapted = true  -->
    <section class="meeting">
      <b-container>
        <TopIndexBanner
          v-if="getTopIndexBanner"
          :title="getTopIndexBanner.title"
          :endtime="getTopIndexBanner.end_date"
          :starttime="getTopIndexBanner.start_date"
          :image="getTopIndexBanner.image"
          :mobile-image="getTopIndexBanner.mobile_image"
          :caseCategory="getTopIndexBanner.case_category"
        />
      </b-container>
    </section>

    <!-- Chests adapted = true -->
    <section class="chests">
      <b-container
        v-for="(cat, key) in getAllChests"
        :key="key"
        :id="key"
      >
        <div class="chests__top">
          <h4 class="chests__title">
            {{ key }}
          </h4>
        </div>
        <transition-group class="row" name="fade" tag="div" mode="out-in">
          <b-col
            v-for="chest in cat"
            :key="chest.id"
            v-show="chest[`${getPlatform}Price`]"
            xl="3"
            lg="4"
            md="6"
            sm="6"
            cols="6"
          >
            <nuxt-link :to="`/case/${chest.id}`" class="chest" tag="div">
              <div class="chest__limited" v-if="chest.isLimited">
                {{ chest.currentOpen }}/{{ chest.maxOpen }}
              </div>
              <img :src="chest.image" alt="" class="chest__img">
              <div class="chest__text">
                <span class="name">{{ chest.name }}</span>
                <div class="chest__discount">
                  <span class="discount">${{ chest[`${getPlatform}Price`] }}</span>
                  <span v-show="chest.oldPrice" class="discount-old">${{ chest.oldPrice }}</span>
                </div>
              </div>
            </nuxt-link>
          </b-col>
        </transition-group>
      </b-container>
    </section>

    <!-- Try  adapted = true -->
    <section class="try">
      <b-container>
        <BottomIndexBanner
          v-if="getBottomIndexBanner"
          :title="getBottomIndexBanner.title"
          :mobile-image="getBottomIndexBanner.mobile_image"
          :image="getBottomIndexBanner.image"
          :case-id="getBottomIndexBanner.case_id"
          :text_en="getBottomIndexBanner.text_en"
          :text_ru="getBottomIndexBanner.text_ru"
        />
      </b-container>
    </section>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import showNotification from '@/mixins/showNotification'
import TopIndexBanner from '@/components/TopBanner'
import BottomIndexBanner from '@/components/BottomBanner'
export default {
  layout: 'default',
  components: {
    TopIndexBanner,
    BottomIndexBanner
  },
  mixins: [showNotification],
  created () {
    try {
      // await this.$store.dispatch('chest/loadAllIndexPage')
    } catch (e) {
      // this.showNotification(this.$t('smtWrong'), 'danger')
    }
  },
  computed: {
    ...mapGetters({
      getWindowSize: 'common/getWindowSize',
      getAllChests: 'chest/getAllChests',
      getPlatform: 'common/getPlatform',
      getTopIndexBanner: 'banners/getTopIndexBanner',
      getBottomIndexBanner: 'banners/getBottomIndexBanner'
    })
  }
}
</script>

<style lang="sass">
@import '@/theme/_mix.sass'
.meeting
  padding: 0 0 32px
  +lg
    padding: 0 0 16px
.chests
  padding-bottom: 16px
  +lg
    padding: 16px 0 0
  &__top
    display: flex
    align-items: center
    justify-content: space-between
    margin-bottom: 32px
  &__title
    font-size: 24px
    line-height: 32px
    letter-spacing: -1px
.chest
  display: flex
  flex-direction: column
  margin-bottom: 32px
  cursor: pointer
  position: relative
  +lg
    margin-bottom: 16px
  &__limited
    position: absolute
    top: 8px
    left: 8px
    z-index: 6
    font-size: 14px
    font-weight: 500
    color: white
  &__img
    width: 100%
    position: relative
    z-index: 5
    height: 216px
    +media-b(500)
      height: auto
  &__discount
    position: relative
    z-index: 10
    +sm
      display: flex
      flex-direction: row-reverse
      align-items: center
      justify-content: space-between
      width: 100%
  &__text
    position: relative
    margin-top: -27px
    padding: 26px 24px
    border-radius: 12px
    box-shadow: 0 8px 8px -4px rgba(20, 16, 41, 0.24), 0 2px 4px -1px rgba(20, 16, 41, 0.24), 0 0 1px 0 rgba(20, 16, 41, 0.4)
    background-color: #27273e
    display: flex
    align-items: center
    justify-content: space-between
    +sm
      padding: 16px
      flex-direction: column
      align-items: flex-start
      margin-top: -18px
    .name
      color: white
      font-size: 20px
      font-weight: 600
      +sm
        margin-bottom: 8px
    .discount
      color: rgba(20, 16, 41, 0.8)
      font-size: 18px
      font-weight: bold
      padding: 6px 16px
      background: #00ffaa
      border-radius: 12px 3px 12px 3px
      +sm
        padding: 4px 8px
        font-size: 13px
        line-height: 16px
      &-old
        color: white
        font-size: 13px
        line-height: 16px
        padding: 4px 8px
        background: #00bbff
        border-radius: 12px 3px 12px 3px
        position: absolute
        right: 0
        top: 0
        transform: translateY(calc(-100% - 12px))
        z-index: 5
        text-decoration: line-through
        +sm
          position: initial
          transform: translateY(0)
          background: none

.try
  padding-bottom: 48px

</style>
