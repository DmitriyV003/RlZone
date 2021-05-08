<template>
  <b-col
    xl="2"
    lg="2"
    md="3"
    sm="4"
    cols="6"
    class="drop__col"
  >
    <div class="drop">
      <span :style="{ background: itemColor }" class="drop__circle" />
      <span :style="{ 'background': color }" class="drop__line" />
      <img :src="imgUrl" alt="" class="drop__img">
      <div class="drop__text">
        <p class="drop__name">
          {{ name }}
        </p>
        <p class="drop__desc">
          {{ desc }}
        </p>
      </div>
    </div>
  </b-col>
</template>

<script>
export default {
  props: {
    color: {
      type: String,
      required: false,
      default: 'blue'
    },
    imgUrl: {
      type: String,
      required: true
    },
    name: {
      type: String,
      required: true
    },
    desc: {
      type: String,
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
.drop
  display: flex
  flex-direction: column
  justify-content: space-between
  max-height: 160px
  border-radius: 12px
  box-shadow: 0 8px 8px -4px rgba(20, 16, 41, 0.24), 0 2px 4px -1px rgba(20, 16, 41, 0.24), 0 0 1px 0 rgba(20, 16, 41, 0.4)
  background-color: #27273e
  padding-top: 16px
  position: relative
  overflow: hidden
  margin-bottom: 32px
  +lg
    margin-bottom: 16px
  &__circle
    width: 16px
    height: 16px
    border-radius: 50%
    position: absolute
    top: 8px
    right: 8px
    z-index: 5
  &__line
    width: 100%
    height: 4px
    bottom: 0
    left: 0
    position: absolute
  &__col
    +lg
      padding: 0 7px
  &__row
    +lg
      margin: 0 -7px
  &__img
    align-self: center
    width: 90px
    height: 68px
  &__text
    padding: 0 16px 16px 16px
    font-size: 13px
    line-height: 16px
    margin-top: 16px
  &__name
    letter-spacing: 1px
    color: white
    font-weight: 600
    margin-bottom: 4px
    overflow: hidden
    white-space: nowrap
    text-overflow: ellipsis
  &__desc
    color: rgba(224, 224, 255, 0.6)
    overflow: hidden
    text-overflow: ellipsis
    white-space: nowrap
</style>
