<template>
  <div class="myParallax">
    <kinesis-element :strength="10" axis="y">
      <div class="bubble bubble-1" />
    </kinesis-element>
    <kinesis-element :strength="12" axis="x">
      <div class="bubble bubble-2" />
    </kinesis-element>
    <kinesis-element :strength="14">
      <div class="bubble bubble-3" />
    </kinesis-element>
    <kinesis-element :strength="12" axis="y">
      <div class="bubble bubble-4" />
    </kinesis-element>
    <kinesis-element :strength="14" axis="x">
      <div class="bubble bubble-5" />
    </kinesis-element>
    <kinesis-element :strength="16">
      <div class="bubble bubble-6" />
    </kinesis-element>
    <kinesis-element :strength="18" axis="y">
      <div class="bubble bubble-7" />
    </kinesis-element>
    <kinesis-element :strength="12">
      <div class="bubble bubble-8" />
    </kinesis-element>
    <kinesis-element :strength="10" axis="x">
      <div class="bubble bubble-9" />
    </kinesis-element>
    <kinesis-element :strength="11" axis="y">
      <div class="bubble bubble-10" />
    </kinesis-element>
    <kinesis-element :strength="18">
      <div class="bubble bubble-11" />
    </kinesis-element>
    <kinesis-element :strength="13" axis="x">
      <div class="bubble bubble-12" />
    </kinesis-element>
    <kinesis-element :strength="11">
      <div class="bubble bubble-13" />
    </kinesis-element>
    <kinesis-element :strength="17" axis="x">
      <div class="bubble bubble-14" />
    </kinesis-element>
  </div>
</template>

<script>
export default {

}
</script>

<style lang="sass">
@import '@/theme/_mix.sass'
.myParallax
  width: 100%
  position: absolute
  left: 0
  top: 70px
  +lg
    display: none
  .bubble
    border-radius: 50%
    background-image: linear-gradient(160deg, #00ffaa, #00bbff 53%, #4579f5)
    position: absolute
    &-1
      width: 35px
      height: 35px
      left: -10px
      top: 0
    &-2
      width: 32px
      height: 32px
      left: 154px
      top: 28px
    &-3
      width: 32px
      height: 32px
      left: 51px
      top: 198px
    &-4
      width: 96px
      height: 96px
      left: 154px
      top: 162px
    &-5
      width: 64px
      height: 64px
      left: 67px
      top: 293px
    &-6
      width: 32px
      height: 32px
      left: 435px
      top: 78px
    &-7
      width: 64px
      height: 64px
      left: 660px
      top: 80px
    &-8
      width: 32px
      height: 32px
      left: 724px
      top: 45px
    &-9
      width: 32px
      height: 32px
      left: 909px
      top: 26px
    &-10
      width: 64px
      height: 64px
      right: 146px
      top: 5px
    &-11
      width: 55px
      height: 55px
      right: -15px
      top: 30px
    &-12
      width: 128px
      height: 128px
      right: 129px
      top: 220px
    &-13
      width: 64px
      height: 64px
      right: 23px
      top: 180px
    &-14
      width: 32px
      height: 32px
      right: 23px
      top: 300px

</style>
