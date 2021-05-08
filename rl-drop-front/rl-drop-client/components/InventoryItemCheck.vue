<template>
  <b-col xl="2" lg="2" md="3" sm="4" cols="6">
    <span :style="{ background: itemColor }" class="inventoryItemCheck__circle" />
    <div :style="{ background: color }" class="inventoryItemCheck__line" />
    <input
      :id="itemId"
      :value="itemId"
      @input="$emit('input', Number($event.target.value))"
      class="inventoryItemCheck__input"
      type="radio"
      name="inventoryItem"
    >
    <label :for="itemId" class="inventoryItemCheck__label">
      <div class="inventoryItemCheck">
        <div class="inventoryItemCheck__check">
          <CheckIcon class="icon" />
        </div>

        <img :src="img" alt="" class="inventoryItemCheck__img">
        <div class="inventoryItemCheck__text">
          <p class="inventoryItemCheck__name">
            {{ name }}
          </p>
          <p class="inventoryItemCheck__desc">
            {{ desc }}
          </p>
          <p class="inventoryItemCheck__price">
            ${{ price }}
          </p>
        </div>
      </div>
    </label>
  </b-col>
</template>

<script>
import CheckIcon from 'vue-material-design-icons/Check.vue'
export default {
  components: {
    CheckIcon
  },
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
    color: {
      type: String,
      required: true
    },
    price: {
      type: Number,
      required: true
    },
    itemId: {
      type: Number,
      required: true
    },
    itemColor: {
      type: String,
      required: true
    }
  }
}
</script>

<style lang="sass">
@import '@/theme/_mix.sass'
.inventoryItemCheck
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
  width: 100%
  &__circle
    width: 16px
    height: 16px
    border-radius: 50%
    position: absolute
    top: 8px
    right: 22px
    z-index: 5
  &__line
    height: 4px
    width: calc(100% - 30px)
    position: absolute
    bottom: 0
    left: 15px
    z-index: 55
    border-radius: 0 0 24px 24px
  &__col
    &_checked
      .inventoryItemCheck
        background: rgba(0, 187, 255, 0.03) !important
        &__check
          display: block !important
  &__input
    display: none
  &__label
    width: 100%
    position: relative
    display: flex
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
    overflow: hidden
    text-overflow: ellipsis
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
  &__check
    position: absolute
    top: 16px
    left: 16px
    display: flex
    align-items: center
    justify-content: center
    width: 24px
    height: 24px
    border-radius: 50%
    background: #00bbff
    color: white
    font-size: 20px
    display: none
  .icon
    height: 15px
</style>
