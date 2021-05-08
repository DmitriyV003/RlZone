<template>
  <div>
    <!-- Advanced adapted = true -->
    <section class="advanced">
      <b-container>
        <b-row>
          <b-col xl="12" lg="12" md="12" sm="12" class="m-auto">
            <div class="open" v-if="getCurrentChest">
              <div class="open__layer">
                <div class="open__line"></div>
              </div>
              <transition name="fade" mode="out-in">
                <img :src="getCurrentChest.chest.backgroundImage" alt="" class="open__bg" key="image">
              </transition>
              <h2 class="open__title" v-if="getCurrentChest" v-show="status === 0 || status === 2">
                {{ getCurrentChest.chest.name }}
              </h2>
              <img v-if="getCurrentChest" v-show="status === 0" :src="getCurrentChest.chest.image" alt="" class="open__chestImage">
              <div class="open__slider" v-show="status === 1" v-if="getCurrentChest">
                <div
                  class="slider__wrapper"
                  v-if="sliderItems.length > 0"
                  :style="{ transform: 'translate3d(-' + this.distance + 'px, 0, 0)' }"
                >
                  <Weapon
                    v-for="item in sliderItems"
                    class="slider__item"
                    :color="item.type.color"
                    :img-url="item.image"
                    :name="item.name"
                    :desc="item.text"
                    :item-color="item.color"
                    :key="item.id"
                    :id="item.isWin ? 'winItem' : ''"
                  />
                </div>
              </div>
              <WinItem
                v-if="getWinItem && status === 2"
                :item-color="getWinItem.color"
                :color="getWinItem.type.color"
                :desc="getWinItem.text"
                :img-url="getWinItem.image"
                :name="getWinItem.name"
                :key="getWinItem.name"
              />
              <div class="open__btns" v-if="getCurrentChest">
                <button
                  v-if="status === 0"
                  @click.prevent="openChest(getCurrentChest.chest.id)"
                  class="btn btn_primary"
                  :disabled="checkBalance || Number(getCurrentChest.chest[`${getPlatform}Price`]) === 0"
                  :class="{'btn_primary_disabled': checkBalance || Number(getCurrentChest.chest[`${getPlatform}Price`]) === 0}"
                >
                  {{ $t('openChest') }} ${{ getCurrentChest.chest[`${getPlatform}Price`] }}
                </button>
                <button @click.prevent="continuePlay" v-if="status === 2" class="btn btn_secondary btn-arrow">
                  <span>{{ $t('continue') }}</span><ArrowRightIcon class="btn__icon" />
                </button>
                <button
                  v-if="getWinItem && status === 2"
                  @click.prevent="sellItem(getWinItem.id)"
                  class="btn btn_primary"
                  :class="!getToken ? 'btn_primary_disabled' : ''"
                  :disabled="!getToken"
                >
                  {{ $t('sell') }} ${{ getWinItem[`${getPlatform}Price`] }}
                </button>
              </div>
            </div>
          </b-col>
        </b-row>
      </b-container>
    </section>

    <!-- Items  adapted =  -->
    <section class="items">
      <b-container v-if="getCurrentChest">
        <b-row class="drop__row">
          <Weapon
            :key="item.id"
            v-for="item in getCurrentChest.items"
            :color="item.type.color"
            :img-url="item.image"
            :name="item.name"
            :desc="item.text"
            :item-color="item.color"
            class="items__drop"
          />
        </b-row>
      </b-container>
    </section>
  </div>
</template>

<script>
import { mapGetters, mapMutations } from 'vuex'
import showNotification from '@/mixins/showNotification'
import ArrowRightIcon from 'vue-material-design-icons/ArrowRight.vue'
import Weapon from '@/components/Weapon'
import { eventBus } from '@/plugins/event-bus'
import WinItem from '@/components/WinItem'
import $ from 'jquery'
export default {
  layout: 'default',
  mixins: [showNotification],
  components: {
    Weapon,
    ArrowRightIcon,
    WinItem
  },
  data () {
    return {
      disabled: true,
      animated: false,
      isSliderActive: false,
      isShowWinItem: false,
      sliderItems: [],
      distance: 0,
      status: 0
    }
  },
  async created () {
    this.$store.commit('chest/setWinItem', null)
    try {
      await this.$store.dispatch('chest/loadItemsForChest', this.$route.params.id)
    } catch (e) {
      this.showNotification('Something went wrong(', 'danger')
    }
  },
  computed: {
    ...mapGetters({
      getCurrentChest: 'chest/getCurrentChest',
      getWinItem: 'chest/getWinItem',
      getPlatform: 'common/getPlatform',
      getToken: 'getToken',
      getUser: 'user/getUser'
    }),
    checkBalance () {
      return !this.getToken || (this.getUser.balance < this.getCurrentChest.chest[`${this.getPlatform}Price`])
    },
    swiper () {
      return this.$refs.mySwiper.$swiper
    }
  },
  mounted () {
    eventBus.$emit('closeMenu')
  },
  methods: {
    ...mapMutations({
      setWinItem: 'chest/setWinItem'
    }),
    continuePlay () {
      this.setWinItem(null)
      this.status = 0
      this.sliderItems = []
      this.distance = 0
    },
    emitOpenChest () {
      const item = {
        userId: this.getUser.id,
        winItem: this.getWinItem
      }
      this.$socket.emit('openChest', item)
    },
    async openChest (id) {
      try {
        this.status = 1
        const data = new FormData()
        data.append('platform', this.getPlatform)
        data.append('id', id)
        const result = await this.$store.dispatch('chest/openChest', data)
        if (result.success) {
          this.emitOpenChest()
          this.showNotification('Chest opened!', 'success')
          const winItemNumber = Math.floor(Math.random() * (50 - 10))
          this.sliderItems = this.getCurrentChest.items
          const items = this.getCurrentChest.items.map((item) => {
            return item
          })
          const itemQuantity = items.length
          const chestItems = this.getCurrentChest.items
          if (items.length < 50) {
            const border = 50 - items.length
            for (let i = 0; i < border; i++) {
              const num = Math.floor(Math.random() * itemQuantity)
              items.push(chestItems[num])
            }
          }
          const winItem = result.data
          winItem.isWin = true
          items[winItemNumber] = winItem
          this.sliderItems = items
          setTimeout(() => {
            this.animateSlider()
          }, 200)
        }
      } catch (e) {
        this.showNotification(e.data.message, 'danger')
      }
    },
    async sellItem (id) {
      try {
        const data = new FormData()
        data.append('platform', this.getPlatform)
        data.append('id', id)
        data.append('userId', this.getUser.id)
        await this.$store.dispatch('item/sell', data)
        this.status = 0
        this.sliderItems = []
        this.distance = 0
      } catch (e) {
        this.showNotification(this.$t('smtWrong'), 'danger')
      }
    },
    animateSlider () {
      // const foundItem = this.getCurrentChest.items.find(curVal => curVal.id === this.getWinItem.id)
      // const index = this.getCurrentChest.items.indexOf(foundItem)
      // const winIndex = (document.querySelector('.swiper-slide[index=' + String(this.getWinItem.id) + ']')).getAttribute('data-swiper-slide-index')
      // console.log(winIndex)
      // console.log(index)
      // console.log(foundItem)
      // const i = this.getCurrentChest.items.length
      const bannerWidth = $('.open').outerWidth()
      const itemPosition = $('#winItem').position().left
      const randomNum = Math.floor(Math.random() * 110 + 60)
      const totalTranslate = -bannerWidth / 2 + itemPosition + randomNum
      setTimeout(() => {
        this.distance = totalTranslate

        setTimeout(() => {
          this.status = 2
        }, 3000)
      }, 300)
    }
  }
}
</script>

<style lang="sass">
@import '@/theme/_mix.sass'
.advanced
  padding: 0 0 32px
  +lg
    padding: 0 0 16px
.items
  padding-bottom: 16px
  +lg
    padding-bottom: 32px
  &__drop
    .drop
      margin-bottom: 32px
.slider
  &__wrapper
    display: flex
    align-items: center
    transition: all 2000ms ease
  &__item
    max-width: 190px !important
.open
  display: flex
  align-items: flex-start
  justify-content: space-between
  flex-wrap: wrap
  padding: 40px
  position: relative
  border-radius: 12px
  box-shadow: 0 8px 8px -4px rgba(20, 16, 41, 0.24), 0 2px 4px -1px rgba(20, 16, 41, 0.24), 0 0 1px 0 rgba(20, 16, 41, 0.4)
  background-color: #27273e
  min-height: 200px
  +lg
    flex-direction: column
    background: url('/images/chest-bg.png')
    background-repeat: no-repeat
    background-size: cover
    background-position: center top
  .drop
    margin-bottom: 0
  &__layer
    position: absolute
    top: 0
    left: 0
    width: 100%
    height: 100%
    display: flex
    align-items: center
    justify-content: center
  &__line
    width: 4px
    background-color: #00ffaa
    height: 100%
  &__activeItem
    .drop
      box-shadow: 0 8px 8px -4px rgba(0, 187, 255, 0.06), 0 16px 24px 0 rgba(0, 187, 255, 0.12), 0 2px 4px -1px rgba(27, 10, 82, 0.06), 0 0 1px 0 rgba(0, 187, 255, 0.12), inset 0 2px 6px 0 rgba(0, 187, 255, 0.4) !important
  &__slider
    width: 100%
    display: block
  &__sliderItem
    width: 190px
    max-width: 190px
  &__title
    position: relative
    z-index: 5
    +lg
      margin-bottom: 20px
  &__chestImage
    max-width: 350px
    width: 100%
    z-index: 9
    height: 261px
    margin-top: -82px
    margin-bottom: -59px
    +lg
      width: 100%
      height: 204px
      margin-top: 0
      margin-bottom: 65px
      max-width: 100%
  &__bg
    position: absolute
    top: 0
    left: 0
    border-radius: 12px
    width: 100%
    height: 100%
    +lg
      display: none
  &__btns
    position: relative
    z-index: 5
    display: flex
    flex-direction: column
    align-items: flex-end
    +lg
      width: 100%
    button
      +lg
        width: 100%
        justify-content: center
      &:not(:last-child)
        margin-bottom: 24px

</style>
