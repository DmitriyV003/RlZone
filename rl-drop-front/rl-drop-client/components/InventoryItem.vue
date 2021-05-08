<template>
  <b-col xl="2" lg="2" md="3" sm="4" cols="6">
    <div
      @click.stop="decoration === true ? false : showSell = true"
      @mouseleave="closeAll"
      @mouseover="status"
      class="inventoryItem"
    >

      <span :style="{ background: itemColor }" class="inventoryItem__circle" />
      <div :style="{ background: color }" class="inventoryItem__line" />

      <div v-if="showSell && withdrawStatusData === null" class="inventoryItem__click">
        <p class="inventoryItem__name">
          {{ name }}
        </p>
        <p class="inventoryItem__desc">
          {{ desc }}
        </p>
        <button @click.prevent="sell" class="btn btn_primary inventoryItem__btn">
          Sell
        </button>
        <button @click.prevent.once="withdraw({ id, platform, pivotId, userId: getUser.id })" class="btn btn_trans inventoryItem__acc">
          To Account
        </button>
      </div>

      <div v-if="showStatus && withdrawStatusData !== null" class="inventoryItem__click">
        <span class="btn btn_primary inventoryItem__btn">
          {{ withdrawStatusData }}
        </span>
      </div>

      <div v-if="showConfirm && withdrawStatusData === null" @mouseleave="showConfirm = false" class="inventoryItem__click">
        <p class="inventoryItem__name">
          Sell this item
        </p>
        <p class="inventoryItem__desc inventoryItem__desc_emp">
          for ${{ price }}?
        </p>
        <button @click="confirmSell({ id, platform, pivotId, price, userId: getUser.id })" class="btn btn_primary inventoryItem__btn">
          Confirm
        </button>
        <button @click.prevent="showConfirm = false" class="btn btn_trans inventoryItem__acc">
          Cancel
        </button>
      </div>

      <img :src="img" alt="" class="inventoryItem__img">
      <div class="inventoryItem__text">
        <p class="inventoryItem__name">
          {{ name }}
        </p>
        <p class="inventoryItem__desc">
          {{ desc }}
        </p>
        <p class="inventoryItem__price">
          ${{ price }}
        </p>
      </div>
    </div>
  </b-col>
</template>

<script>
import { eventBus } from '@/plugins/event-bus'
import showNotification from '@/mixins/showNotification'
import { mapGetters } from 'vuex'

export default {
  props: {
    name: {
      type: String,
      required: true
    },
    desc: {
      type: String,
      required: true
    },
    img: {
      type: String,
      required: true
    },
    price: {
      type: Number,
      required: true
    },
    id: {
      type: Number,
      required: true
    },
    platform: {
      type: String,
      required: true
    },
    pivotId: {
      type: Number,
      required: true
    },
    itemColor: {
      type: String,
      required: true
    },
    color: {
      type: String,
      required: true
    },
    withdrawStatus: {
      type: null,
      required: true
    },
    decoration: {
      type: Boolean,
      default: false
    }
  },
  mixins: [showNotification],
  computed: {
    ...mapGetters({
      getUser: 'user/getUser'
    })
  },
  data () {
    return {
      showSell: false,
      showConfirm: false,
      showStatus: false,
      withdrawStatusData: this.withdrawStatus
    }
  },
  methods: {
    sell () {
      if (!this.decoration) {
        this.showSell = false
        this.showConfirm = true
      }
    },
    confirmSell (data) {
      if (!this.decoration) {
        eventBus.$emit('sellItem', data)
      }
    },
    status () {
      if (this.withdrawStatusData !== null) {
        this.showStatus = true
        this.showSell = false
        this.showConfirm = false
      }
    },
    closeAll () {
      this.showStatus = false
      this.showSell = false
      this.showConfirm = false
    },
    async withdraw (payload) {
      try {
        if (!this.decoration) {
          await this.$store.dispatch('user/withdraw', payload)
          this.showNotification('Your application to withdraw accepted!', 'info')
          this.withdrawStatusData = 'pending'
        }
      } catch (e) {

      }
    }
  }

}
</script>

<style lang="sass">
@import '@/theme/_mix.sass'
.inventoryItem
  +item_dark
  padding: 16px
  position: relative
  overflow: hidden
  display: flex
  flex-direction: column
  align-items: center
  border-radius: 12px
  max-height: 180px
  cursor: pointer
  &__circle
    width: 16px
    height: 16px
    border-radius: 50%
    position: absolute
    top: 8px
    right: 8px
    z-index: 5
  &__line
    height: 4px
    width: calc(100%)
    position: absolute
    bottom: 0
    left: 0
    z-index: 55
    border-radius: 0 0 24px 24px
  &__click
    position: absolute
    top: 0
    left: 0
    width: 100%
    height: calc(100% - 4px)
    border-radius: 12px 12px 0 0
    background-color: #323249
    z-index: 10
    padding: 16px
  &__btn
    width: 100%
    padding: 10px
    margin: 16px 0 16px
    font-size: 13px
    line-height: 16px
  &__col
    margin-bottom: 32px
    +lg
      margin-bottom: 16px
      padding: 0 7px
  &__row
    +lg
      margin: 0 -7px
  &__img
    margin-bottom: 16px
    width: 96px
    height: 52px
  &__text
    display: flex
    flex-direction: column
    align-items: flex-start
    font-size: 13px
    line-height: 16px
    width: 100%
    color: white
    overflow: hidden
  &__name
    color: white
    letter-spacing: 1px
    font-weight: 600
    margin-bottom: 4px
    font-size: 13px
    line-height: 16px
  &__desc
    color: rgba(224, 224, 255, 0.6)
    font-weight: 500
    font-size: 13px
    line-height: 16px
    white-space: nowrap
    text-overflow: ellipsis
    overflow: hidden
    &_emp
      color: white
      font-weight: 600
  &__acc
    color: #00bbff
    font-size: 13px
    line-height: 16px
    width: 100%
    background: rgba(0, 187, 255, 0.03)
    border-radius: 18px
    padding: 10px
    &:hover
      color: rgba(0, 187, 255, 0.6)
  &__price
    margin-top: 16px
    align-self: flex-end
    padding: 4px 8px
    color: rgba(20, 16, 41, 0.8)
    background: #00ffaa
    border-radius: 12px 3px 12px 3px
    font-weight: bold
</style>
