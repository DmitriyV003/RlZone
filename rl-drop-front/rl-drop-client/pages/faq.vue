<template>
  <section class="faq">
    <b-container>
      <div class="faq__title">
        <h2>FAQ</h2>
        <div v-if="getWindowSize > 991" class="faq__filter">
          <span v-for="opt in options" @click="changeFilter(opt)">{{ opt }}</span>
        </div>
        <multiselect
          v-model="value"
          v-else
          :options="options"
          :searchable="false"
          :allowEmpty="false"
          :showLabels="false"
          :hideSelected="true"
          class="faq__select"
        />
      </div>
      <b-row v-if="getFaqs.length > 0">
        <Faq
          v-for="faq in getFaqs"
          :key="faq.id"
          :title="faq[`title_${$i18n.locale}`]"
          :text="faq[`text_${$i18n.locale}`]"
          v-show="faq[`category_${$i18n.locale}`] === value"
        />
      </b-row>
    </b-container>
  </section>
</template>

<script>
import { mapGetters } from 'vuex'
import { eventBus } from '@/plugins/event-bus'
import showNotification from '@/mixins/showNotification'
import Faq from '../components/Faq'
export default {
  layout: 'default',
  components: {
    Faq
  },
  mixins: [showNotification],
  data () {
    return {
      value: 'General Questions',
      options: []
    }
  },
  mounted () {
    // try {
    //   const result = await this.$store.dispatch('faq/loadFaqs')
    //   this.options = result.options
    //   this.value = result.options[0]
    // } catch (e) {
    //   this.showNotification(this.showNotification(this.$t('smtWrong'), 'danger'))
    // }
    eventBus.$emit('closeMenu')
  },
  computed: {
    ...mapGetters({
      getWindowSize: 'common/getWindowSize',
      getFaqs: 'faq/getFaqs'
    })
  },
  methods: {
    changeFilter (filter) {
      this.value = filter
    }
  }
}
</script>

<style lang="sass">
@import '../theme/_mix.sass'
.faq
  padding: 16px 0 32px
  .multiselect
    &__tags
      min-height: initial
      padding: 6px 42px 6px 12px
      border-radius: 18px
      box-shadow: 0 8px 8px -4px rgba(0, 187, 255, 0.12), 0 16px 24px 0 rgba(0, 187, 255, 0.24), 0 2px 4px -1px rgba(10, 70, 82, 0.12), 0 0 1px 0 rgba(0, 187, 255, 0.24)
      background-image: linear-gradient(101deg, #00ffaa, #00bbff 53%, #4579f5)
      border: none
      cursor: pointer
    &__element
      font-size: 14px
      line-height: 18px
    &__single
      margin-bottom: 0
      padding: 0
      background: transparent
      color: white
      font-size: 14px
      font-weight: 500
      line-height: 24px
    &__placeholder
      margin-bottom: 0
      padding: 0
      background: transparent
      color: white
      font-size: 14px
      font-weight: 500
      line-height: 24px
    &__select
      height: 32px
      &::before
        border-color: white transparent transparent
</style>
