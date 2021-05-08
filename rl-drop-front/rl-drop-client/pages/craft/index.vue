<template>
  <div>
    <!-- Craft items  adapted =  -->
    <section class="craftItems">
      <b-container>
        <b-row class="align-items-center justify-content-lg-center">
          <b-col
            class="craftProgress__col"
            xl="4"
            offset-xl="1"
            lg="5"
          >
            <TriangleIcon v-if="!craftStatus || craftStatus === 1 || craftStatus === 2" class="craftProgress__triangle" />
            <TriangleIcon v-if="craftStatus === 4" class="craftProgress__triangle craftProgress__triangle_danger" />
            <TriangleIcon v-if="craftStatus === 3" class="craftProgress__triangle craftProgress__triangle_success" />
            <div class="craftProgress">
              <div v-if="getCurrentCraftItem">
                <circleBar
                  :dash-spacing="0"
                  :stroke-width="2"
                  :active-width="4"
                  :active-count="activeCount"
                  size="10rem"
                  active-stroke="#00bbff"
                  stroke="transparent"
                  class="craft__progressLine"
                />
              </div>
              <div v-if="getCurrentCraftItem ">
                <circleBar
                  :stroke-width="100"
                  :active-width="100"
                  :dash-spacing="0"
                  :active-count="activeCount"
                  size="10rem"
                  stroke="transparent"
                  class="craft__progressLine"
                  active-stroke="rgba(0, 187, 255, 0.08)"
                />
              </div>
              <div class="craftProgress__border">
                <span class="main" />
                <span class="center" />
              </div>
              <div
                class="craftProgress__progress"
                :class="{ 'craftProgress__progress_initial': !getCurrentCraftItem }"
                :style="{ transform: `rotate(${activeCircleCount}deg) translateX(175px)` }"
              >
                <span class="rotate">
                  <span>{{ progressBar }}</span> <span class="emp"> %</span>
                </span>
              </div>
              <div v-if="getCurrentCraftItem" class="craftProgress__circle">
                <img :src="getCurrentCraftItem.image" alt="" class="craftProgress__img">
              </div>
              <div v-else class="craftProgress__empty">
                <p>Choose an</p>
                <p>item below</p>
              </div>
            </div>
          </b-col>
          <b-col xl="6" offset-xl="1" lg="7">
            <div class="craft">
              <h1 class="craft__title">
                {{ $t('craftItem') }}
              </h1>
              <div class="craft__slider">
                <div class="craft__top">
                  <span class="craft__desc">{{ $t('probOfWinning') }}</span>
                  <span class="craft__progress"> {{ progress }} %</span>
                </div>
                <client-only>
                  <vue-slide-bar
                    :lineHeight="8"
                    v-model="progress"
                    :min="5"
                    :max="75"
                    :is-disabled="true"
                    :draggable="(craftStatus === 1 || craftStatus === 3 || craftStatus === 4) && typeof getToken !== 'undefined'"
                    @input="input"
                  />
                </client-only>
              </div>
              <div class="craft__bottom">
                <button
                  @click.prevent="play"
                  :disabled="getBtnState || !getCurrentCraftItem"
                  :class="getBtnState ? 'btn_primary_disabled' : ''"
                  v-if="craftStatus === 1 || craftStatus === 0 || craftStatus === 2"
                  class="btn btn_primary"
                >
                  {{ $t('play') }} ${{ Number(price * progress / 100).toFixed(2) }}
                </button>
                <button
                  @click.prevent="continueCraft"
                  :disabled="getBtnState"
                  :class="getBtnState ? 'btn_primary_disabled' : ''"
                  v-if="craftStatus === 4"
                  class="btn btn_primary"
                >
                  {{ $t('playAgain') }}
                </button>
                <button
                  :disabled="getBtnState"
                  :class="getBtnState ? 'btn_secondary_disabled' : ''"
                  @click="sellItem(getCurrentCraftItem.id)"
                  v-if="craftStatus === 3"
                  class="btn btn_secondary craft__btn"
                >
                  {{ $t('sell') }} ${{ getCurrentCraftItem[`${getPlatform}Price`] }}
                </button>
                <button
                  :disabled="getBtnState"
                  :class="getBtnState ? 'btn_primary_disabled' : ''"
                  v-if="craftStatus === 3"
                  class="btn btn_primary"
                  @click.prevent="continueCraft"
                >
                  {{ $t('continue') }}
                </button>
              </div>
            </div>
          </b-col>
        </b-row>
      </b-container>
    </section>

    <!-- Inventory  adapted = true -->
    <section class="inventory">
      <b-container>
        <div class="faq__title">
          <h2>{{ $t('choose') }}</h2>
          <div v-if="getWindowSize > 1200 && getTypes.length > 0" class="faq__filter">
            <span
              v-for="(item) in getTypes"
              :key="item.id"
              :class="filterItems.type === item.type && showAll === false ? 'faq__filter_active' : ''"
              @click="showItems(item)"
            >{{ item.type }}</span>
            <span
              :class="showAll ? 'faq__filter_active' : ''"
              @click="showAll = true"
            >All</span>
          </div>
          <client-only v-else-if="getWindowSize <= 1200 && getTypes.length > 0">
            <multiselect
              v-model="filterItems"
              :options="getTypes"
              :searchable="false"
              :allowEmpty="false"
              :showLabels="false"
              :hideSelected="true"
              track-by="type"
              label="type"
              class="inventory__select"
              @input="hideAll"
            />
          </client-only>
        </div>

        <b-row class="inventoryItemCheck__row" v-if="getCraftItems.length > 0">
          <InventoryItemCheck
            v-for="(item) in getCraftItems"
            v-show="item.type.type === filterItems.type || showAll"
            :key="item.id"
            v-model="craftItem"
            :img="item.image"
            :name="item.name"
            :color="item.type.color"
            :price="item[`${getPlatform}Price`]"
            :desc="item.text"
            :item-id="item.id"
            :item-color="item.color"
            :class="craftItem === item.id ? 'inventoryItemCheck__col_checked' : ''"
            @click.prevent.native="setCraftItem(
              { itemId: item.id, itemName: item.name, platform: getPlatform },
              item.id)"
            class="inventoryItemCheck__col"
          />
        </b-row>
      </b-container>
    </section>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import { eventBus } from '@/plugins/event-bus'
import showNotification from '@/mixins/showNotification'
import TriangleIcon from 'vue-material-design-icons/Triangle.vue'
import InventoryItemCheck from '../../components/InventoryItemCheck'
export default {
  layout: 'default',
  components: {
    InventoryItemCheck,
    TriangleIcon
  },
  mixins: [showNotification],
  data () {
    return {
      filterItems: { type: 'common' },
      progress: 5,
      progressBar: 0,
      craftItem: null,
      price: null,
      craftStatus: 0,
      craftResult: null,
      showAll: false,
      active () {
        return 60 - (this.progressBar / 100 * 60)
      },
      circleCount () {
        return -90 - this.progressBar * 3.6
      }
    }
  },
  computed: {
    ...mapGetters({
      getWindowSize: 'common/getWindowSize',
      getCurrentCraftItem: 'item/getCurrentCraftItem',
      getToken: 'getToken',
      getBtnState: 'item/getBtnState',
      getCraftItems: 'item/getCraftItems',
      getPlatform: 'common/getPlatform',
      getTypes: 'item/getTypes',
      getUser: 'user/getUser'
    }),
    activeCount: {
      get () {
        return this.active()
      },
      set (value) {
        this.active = value
      }
    },
    activeCircleCount: {
      get () {
        return this.circleCount()
      },
      set (value) {
        this.circleCount = value
      }
    },
    checkBalance () {
      return !this.getToken || (this.getUser.balance < this.getCurrentCraftItem[`${this.getPlatform}Price`])
    }
  },
  async mounted () {
    try {
      await this.$store.dispatch('item/fetchCraftItems')
    } catch (e) {
      this.showNotification('Something went wrong!', 'danger')
    }
    if ('itemId' in this.$route.query) {
      try {
        await this.queryReact(this.$route.query.itemId, true)
        this.craftStatus = 1
        this.progress = 5
      } catch (e) {
        this.showNotification(this.showNotification(this.$t('smtWrong'), 'danger'))
      }
    } else {
      await this.$router.push(this.localePath('craft'))
      this.$store.commit('item/setCurrentCraftItem', null)
    }
    eventBus.$emit('closeMenu')
    eventBus.$off('selectCraftItem')
    eventBus.$on('selectCraftItem', (s) => {
      // this.$router.push(this.localePath({
      //   name: 'craft',
      //   query: s[0]
      // }))
      this.craftStatus = 1
      this.progress = 5
      this.queryReact(s[1], true)
    })
  },
  methods: {
    continueCraft () {
      this.craftStatus = 1
      this.progress = 5
      this.$store.commit('item/setCurrentCraftItem', null)
    },
    hideAll () {
      this.showAll = false
    },
    showItems (item) {
      this.showAll = false
      this.filterItems = item
    },
    setCraftItem (s, id) {
      this.$router.push(this.localePath({
        name: 'craft',
        query: s
      }))
      eventBus.$emit('selectCraftItem', [s, id])
    },
    async queryReact (id, doGet) {
      if (doGet) {
        await this.getItemForCraft(id)
        this.craftItem = id
      }
      const platform = this.$route.query.platform
      if (this.$route.query.itemId && this.getCurrentCraftItem) {
        this.price = this.getCurrentCraftItem[`${platform}Price`]
        this.craftItem = this.getCurrentCraftItem.id
      } else {
        this.$store.commit('item/setCurrentCraftItem', null)
        this.craftItem = null
      }
      this.$store.commit('item/setBtnState', !((this.getToken && this.getCurrentCraftItem)))
    },
    async getItemForCraft (id) {
      try {
        await this.$store.dispatch('item/getItemForCraft', id)
      } catch (e) {
        throw e
      }
    },
    craftAnimation () {
      this.active = () => {
        return (this.progressBar / 100 * 60)
      }
      this.circleCount = () => {
        return -90 + this.progressBar * 3.6
      }
      const endNumber = 100 - this.progress
      const failNumber = Math.floor(Math.random() * this.progress)
      const interval = setInterval(() => {
        if (this.craftResult === false) {
          if (failNumber > this.progressBar) {
            this.progressBar += 1
          } else {
            clearInterval(interval)
            this.craftStatus = 4
            this.showNotification(this.$t('craftFail'), 'danger')
            this.$store.commit('item/setBtnState', false)
          }
        } else if (endNumber + 3 > this.progressBar) {
          this.progressBar += 1
          // this.progressBar *= 0.799999
        } else {
          clearInterval(interval)
          this.craftStatus = 3
          this.showNotification(this.$t('craftSuccess'), 'success')
          this.$store.commit('item/setBtnState', false)
        }
      }, 150)
    },
    input (e) {
      this.active = () => {
        return 60 - (this.progressBar / 100 * 60)
      }
      this.circleCount = () => {
        return -90 - this.progressBar * 3.6
      }
      this.progressBar = this.progress
    },
    async play () {
      if (this.checkBalance) {
        return this.showNotification(this.$t('notEnoughMoney'), 'danger')
      }
      try {
        this.progressBar = 0
        this.craftStatus = 2
        this.$store.commit('user/changeUserBalance', this.price * this.progress / 100 * -1)
        this.$store.commit('user/beautifyBalance', '')
        this.$store.commit('item/setBtnState', true)
        const result = await this.$store.dispatch('item/play', this.progress)
        this.craftResult = result.data
        if (result.success) {
          await this.craftAnimation()
        }
      } catch (e) {
        return this.showNotification(`${e.data.message}`, 'danger')
      }
    },
    async sellItem (id) {
      try {
        const data = new FormData()
        data.append('platform', this.getPlatform)
        data.append('id', id)
        this.craftStatus = 1
        await this.$store.dispatch('item/sell', data)
        this.showNotification(this.$t('soldItem'), 'success')
      } catch (e) {
        this.showNotification(this.$t('smtWrong'), 'danger')
      }
    }
  }
}
</script>

<style lang="sass">
@import '@/theme/_mix.sass'
.inventory
  padding-bottom: 16px
  +lg
    padding-bottom: 32px
  &__top
    padding: 50px 0 34px
    display: flex
    align-items: center
    justify-content: space-between
    margin-bottom: 32px
  &__filter
    display: flex
    align-items: center
  &__filterItem
    padding: 8px 16px
    color: rgba(224, 224, 255, 0.6)
    position: relative
    display: flex
    align-items: center
    font-size: 14px
    font-weight: 500
    cursor: pointer
    user-select: none
    border-radius: 24px
    &:not(:last-child)
      margin-right: 24px
    &_active
      +btn_primary
      color: white
      .quantity
        color: #00bbff
        background: white
    .quantity
      padding: 1px 4px
      background: #00bbff
      border-radius: 64px
      color: white
      margin-left: 9px
      font-size: 13px
      line-height: 16px
.craftItems
  padding: 40px 0 90px
.craft
  padding: 40px
  +item_dark
  border-radius: 12px
  +lg
    margin-top: 32px
  +md
    padding: 24px
  &__btn
    margin-right: 24px
  &__progressLine
    width: 100%
    position: absolute
    top: 0
    left: 0
    height: 100%
  &__title
    margin-bottom: 40px
  &__top
    display: flex
    align-items: center
    justify-content: space-between
    font-size: 14px
    font-weight: 500
    margin-bottom: 12px
  &__desc
    color: rgba(224, 224, 255, 0.6)
  &__progress
    color: white
  &__bottom
    margin-top: 48px
    display: flex
    justify-content: flex-end
  .vue-slide-bar-component
    padding-top: 0 !important
  .vue-slide-bar-process
    background: #00bbff !important
  .vue-slide-bar
    background: rgba(224, 224, 255, 0.06) !important
  .vue-slide-bar-tooltip
    font-size: 0 !important
    line-height: 0 !important
    width: 24px
    height: 24px
    box-shadow: 0 8px 8px -4px rgba(20, 16, 41, 0.06), 0 2px 4px -1px rgba(20, 16, 41, 0.06), 0 0 1px 0 rgba(20, 16, 41, 0.12) !important
    background: white !important
    display: block
    border: none !important
    border-radius: 50% !important
    &::before
      display: none !important
  .vue-slide-bar-tooltip-top
    top: 7px !important
.craftProgress
  width: 350px
  height: 350px
  border-radius: 50%
  box-shadow: 32px 32px 32px -4px rgba(20, 16, 41, 0.24), 64px 64px 96px 0 rgba(20, 16, 41, 0.4), 8px 8px 16px -1px rgba(20, 16, 41, 0.24), 0 0 1px 0 rgba(20, 16, 41, 0.4), -32px -32px 32px -4px rgba(224, 224, 255, 0.02), -64px -64px 96px 0 rgba(224, 224, 255, 0.04), -8px -8px 16px -1px rgba(224, 224, 255, 0.02), 0 0 1px 0 rgba(224, 224, 255, 0.04)
  background-image: linear-gradient(134deg, rgba(224, 224, 255, 0) 6%, rgba(224, 224, 255, 0.12) 173%), linear-gradient(to bottom, #202036, #202036)
  display: flex
  align-items: center
  justify-content: center
  position: relative
  flex-direction: column
  +sm
    width: 276px
    height: 276px
  &__empty
    width: 200px
    height: 200px
    border-radius: 50%
    background-image: linear-gradient(138deg, rgba(0, 187, 255, 0.8) 17%, rgba(0, 187, 255, 0.06) 69%)
    color: white
    letter-spacing: 1px
    font-size: 14px
    line-height: 24px
    display: flex
    align-items: center
    justify-content: center
    font-weight: 600
    flex-direction: column
    text-transform: uppercase
  &__triangle
    color: #33334b
    transform: rotate(180deg)
    font-size: 50px
    margin-bottom: 8px
    &_danger
      color: #ff00aa
    &_success
      color: #00ffaa
  &__col
    display: flex
    justify-content: center
    flex-direction: column
    align-items: center
  &__progress
    width: 72px
    height: 72px
    border-radius: 50%
    box-shadow: 0 8px 8px -4px rgba(0, 187, 255, 0.06), 0 16px 24px 0 rgba(0, 187, 255, 0.12), 0 2px 4px -1px rgba(27, 10, 82, 0.06), 0 0 1px 0 rgba(0, 187, 255, 0.12), inset 0 2px 6px 0 rgba(0, 187, 255, 0.4)
    background-image: linear-gradient(135deg, #33334b, #27273e 50%, #202036)
    color: white
    display: flex
    align-items: center
    justify-content: center
    letter-spacing: -1px
    font-size: 24px
    font-weight: bold
    position: absolute
    z-index: 3
    top: calc(50% - 36px)
    transform: translateX(175px)
    .emp
      font-size: 14px
    &_initial
      transform: rotate(0) translateX(175px) !important
  &__circle
    width: 200px
    height: 200px
    border-radius: 50%
    background-image: linear-gradient(138deg, rgba(224, 224, 255, 0.12) 17%, rgba(224, 224, 255, 0) 70%)
    display: flex
    align-items: center
    justify-content: center
    +sm
      width: 169px
      height: 169px
  &__img
    width: 72%
  &__bg
    position: absolute
    top: 0
    left: 0
    width: 100%
    height: 100%
  &__border
    position: absolute
    top: 0
    left: 0
    width: 100%
    height: 100%
    border-radius: 50%
    .main
      width: 100%
      height: 100%
      border-radius: 50%

.set-size
  font-size: 10em

.charts-container:after
  clear: both
  content: ""
  display: table

.pie-wrapper
  height: 100%
  width: 100%
  float: left
  position: relative

.pie-wrapper:nth-child(3n + 1)
  clear: both

.pie-wrapper .pie
  height: 100%
  width: 100%
  clip: rect(0, 100%, 100%, 50%)
  left: 0
  position: absolute
  top: 0

.pie-wrapper .pie .half-circle
  height: 100%
  width: 100%
  border: 0.1em solid #3498db
  border-radius: 50%
  clip: rect(0, 50%, 100%, 0)
  left: 0
  position: absolute
  top: 0

.pie-wrapper .shadow
  height: 100%
  width: 100%
  border: 0.1em solid #bdc3c7
  border-radius: 50%

.pie-wrapper.style-2 .label
  background: none
  color: #7f8c8d

.pie-wrapper.style-2 .label .smaller
  color: #bdc3c7

.pie-wrapper.progress-30 .pie .half-circle
  border-color: #3498db

.pie-wrapper.progress-30 .pie .left-side
  -webkit-transform: rotate(108deg)
          transform: rotate(108deg)

.pie-wrapper.progress-30 .pie .right-side
  display: none

.pie-wrapper.progress-60 .pie
  clip: rect(auto, auto, auto, auto)

.pie-wrapper.progress-60 .pie .half-circle
  border-color: #9b59b6

.pie-wrapper.progress-60 .pie .left-side
  -webkit-transform: rotate(216deg)
          transform: rotate(216deg)

.pie-wrapper.progress-60 .pie .right-side
  -webkit-transform: rotate(180deg)
          transform: rotate(180deg)

.pie-wrapper.progress-90 .pie
  clip: rect(auto, auto, auto, auto)

.pie-wrapper.progress-90 .pie .half-circle
  border-color: #e67e22

.pie-wrapper.progress-90 .pie .left-side
  -webkit-transform: rotate(324deg)
          transform: rotate(324deg)

.pie-wrapper.progress-90 .pie .right-side
  -webkit-transform: rotate(180deg)
          transform: rotate(180deg)

.pie-wrapper.progress-45 .pie .half-circle
  border-color: #1abc9c

.pie-wrapper.progress-45 .pie .left-side
  -webkit-transform: rotate(162deg)
          transform: rotate(162deg)

.pie-wrapper.progress-45 .pie .right-side
  display: none

.pie-wrapper.progress-75 .pie
  clip: rect(auto, auto, auto, auto)

.pie-wrapper.progress-75 .pie .half-circle
  border-color: #8e44ad

.pie-wrapper.progress-75 .pie .left-side
  -webkit-transform: rotate(270deg)
          transform: rotate(270deg)

.pie-wrapper.progress-75 .pie .right-side
  -webkit-transform: rotate(180deg)
          transform: rotate(180deg)

.pie-wrapper.progress-95 .pie
  clip: rect(auto, auto, auto, auto)

.pie-wrapper.progress-95 .pie .half-circle
  border-color: #e74c3c

.pie-wrapper.progress-95 .pie .left-side
  -webkit-transform: rotate(342deg)
          transform: rotate(342deg)

.pie-wrapper.progress-95 .pie .right-side
  -webkit-transform: rotate(180deg)
          transform: rotate(180deg)

.pie-wrapper--solid
  border-radius: 50%
  overflow: hidden

.pie-wrapper--solid:before
  border-radius: 0 100% 100% 0 / 50%
  content: ""
  display: block
  height: 100%
  margin-left: 50%
  -webkit-transform-origin: left
          transform-origin: left

.pie-wrapper--solid .label
  background: transparent

.pie-wrapper--solid.progress-65
  background: -webkit-gradient(linear, left top, right top, color-stop(50%, #e67e22), color-stop(50%, #34495e))
  background: linear-gradient(to right, #e67e22 50%, #34495e 50%)

.pie-wrapper--solid.progress-65:before
  background: #e67e22
  -webkit-transform: rotate(126deg)
          transform: rotate(126deg)

.pie-wrapper--solid.progress-25
  background: -webkit-gradient(linear, left top, right top, color-stop(50%, #9b59b6), color-stop(50%, #34495e))
  background: linear-gradient(to right, #9b59b6 50%, #34495e 50%)

.pie-wrapper--solid.progress-25:before
  background: #34495e
  -webkit-transform: rotate(-270deg)
          transform: rotate(-270deg)

.pie-wrapper--solid.progress-88
  background: -webkit-gradient(linear, left top, right top, color-stop(50%, #3498db), color-stop(50%, #34495e))
  background: linear-gradient(to right, #3498db 50%, #34495e 50%)

.pie-wrapper--solid.progress-88:before
  background: #3498db
  -webkit-transform: rotate(43.2deg)
          transform: rotate(43.2deg)

</style>
