<template>
  <div>
    <!-- Statistics  adapted = true -->
    <section class="accounntDash">
      <b-container>
        <b-row>
          <b-col xl="4" lg="4" md="6" class="dash__col">
            <div class="dash__item dash__item_wrap">
              <div class="dash__block">
                <label v-if="user">
                  <img :src="user.photo" alt="" class="dash__img">
                </label>
                <img v-else src="/images/avatar.jpg" alt="" class="dash__img">
                <div class="dash__text">
                  <p class="dash__title" v-if="user">
                    {{ user.name }}
                  </p>
                </div>
              </div>
            </div>
          </b-col>
          <b-col xl="4" lg="4" md="6" class="dash__col">
            <div class="dash__item dash__item_wrap">
              <img v-if="bestCase" :src="bestCase.image" alt="" class="dash__img">
              <div class="dash__text">
                <p class="dash__title dash__title_block">
                  {{ $t('bestCase') }}
                </p>
                <p v-if="bestCase" class="dash__desc">
                  {{ bestCase.name }}
                </p>
              </div>
            </div>
          </b-col>
          <b-col xl="4" lg="4">
            <div class="dash__item">
              <img v-if="bestItem" :src="bestItem.image" alt="" class="dash__img">
              <div class="dash__text">
                <p class="dash__title dash__title_block">
                  {{ $t('bestCrop') }}
                </p>
                <p v-if="bestItem" class="dash__desc">
                  {{ bestItem.name }}
                </p>
              </div>
            </div>
          </b-col>
        </b-row>

        <b-row>
          <b-col xl="4" lg="4" md="6" sm="6" cols="6">
            <div class="dashInfo dashInfo_green">
              <p class="dashInfo__num">
                {{ cases }}
              </p>
              <p class="dashInfo__text">
                {{ $t('cases') }}
              </p>
            </div>
          </b-col>
          <b-col xl="4" lg="4" md="6" sm="6" cols="6">
            <div class="dashInfo dashInfo_blue">
              <p class="dashInfo__num">
                {{ crafts }}
              </p>
              <p class="dashInfo__text">
                {{ $t('crafts') }}
              </p>
            </div>
          </b-col>
          <b-col xl="4" lg="4">
            <div class="dashInfo dashInfo_d-blue">
              <p class="dashInfo__num">
                {{ items }}
              </p>
              <p class="dashInfo__text">
                {{ $t('items') }}
              </p>
            </div>
          </b-col>
        </b-row>
      </b-container>
    </section>

    <!-- Inventory  adapted = true -->
    <section class="inventory" v-if="inventory.length > 0" >
      <b-container>
        <div class="inventory__top">
          <h3>{{ $t('inventory') }}</h3>
          <div v-if="getWindowSize >= 991 && options.length > 0" class="inventory__filter">
            <div
              v-for="opt in options"
              :key="opt.platform"
              @click="filterItems = opt"
              :class="filterItems.platform === opt.platform ? 'inventory__filterItem_active' : ''"
              class="inventory__filterItem"
            >
              <span>{{ opt.platform.toUpperCase() }}</span>
              <span class="quantity">{{ opt.count }}</span>
            </div>
          </div>
          <client-only v-else>
            <multiselect
              v-if="options.length > 0"
              v-model="filterItems"
              :options="options"
              :searchable="false"
              :allowEmpty="false"
              :showLabels="false"
              :hideSelected="true"
              track-by="platform"
              class="inventory__select"
            >
              <template slot="singleLabel" slot-scope="props">
                <div class="customLabel">
                  <span class="customLabel__name">{{ props.option.platform }}</span>
                  <span class="customLabel__num">{{ props.option.count }}</span>
                </div>
              </template>
              <template slot="option" slot-scope="props">
                <div class="customLabel">
                  <span class="customLabel__name">{{ props.option.platform }}</span>
                  <span class="customLabel__num">{{ props.option.count }}</span>
                </div>
              </template>
            </multiselect>
          </client-only>
        </div>

        <b-row v-if="inventory.length > 0" class="inventoryItem__row">
          <InventoryItem
            v-for="item in inventory"
            :key="item.pivot.id"
            :withdraw-status="item.pivot.withdrawStatus"
            :pivot-id="item.pivot.id"
            :id="item.id"
            :img="item.image"
            :name="item.name"
            :color="item.type.color"
            :item-color="item.color"
            :platform="item.platform"
            :price="Number(item[`${item.platform}Price`])"
            :desc="item.text"
            :decoration="true"
            v-show="filterItems.platform === item.platform"
            class="inventoryItem__col"
          />
        </b-row>
      </b-container>
    </section >
    <div v-if="inventory.length === 0" class="container mt-5">
      <h3 class="mt-3">
        {{ $t('noItems') }}
      </h3>
    </div>

    <b-container>
      <h3 class="inventory__title">Sold Items</h3>
      <b-row v-if="soldItems.length > 0">
        <InventoryItem
          v-for="item in soldItems"
          :key="item.pivot.id"
          :withdraw-status="item.pivot.withdrawStatus"
          :pivot-id="item.pivot.id"
          :id="item.id"
          :img="item.image"
          :name="item.name"
          :color="item.type.color"
          :item-color="item.color"
          :platform="item.platform"
          :price="Number(item[`${item.platform}Price`])"
          :desc="item.text"
          :decoration="true"
          class="inventoryItem__col"
        />
      </b-row>
    </b-container>

    <b-container>
      <h3 class="inventory__title">Success Withdrew Items</h3>
      <b-row v-if="withdrewItems.length > 0">
        <InventoryItem
          v-for="item in withdrewItems"
          :key="item.pivot.id"
          :withdraw-status="item.pivot.withdrawStatus"
          :pivot-id="item.pivot.id"
          :id="item.id"
          :img="item.image"
          :name="item.name"
          :color="item.type.color"
          :item-color="item.color"
          :platform="item.platform"
          :price="Number(item[`${item.platform}Price`])"
          :desc="item.text"
          :decoration="true"
          class="inventoryItem__col"
        />
      </b-row>
    </b-container>
  </div>
</template>

<script>
import showNotification from '@/mixins/showNotification'
import { mapGetters } from 'vuex'
import { eventBus } from '@/plugins/event-bus'
import InventoryItem from '@/components/InventoryItem'
export default {
  layout: 'default',
  components: {
    InventoryItem
  },
  mixins: [showNotification],
  data () {
    return {
      disabled: true,
      cases: 0,
      crafts: 0,
      items: 0,
      bestItem: null,
      bestCase: null,
      user: null,
      inventory: [],
      options: [],
      soldItems: [],
      withdrewItems: [],
      filterItems: {}
    }
  },
  async mounted () {
    try {
      const result = await this.$axios.$get(`${this.$axios.defaults.baseURL}/user/${this.$route.params.id}`)
      this.cases = result.data.cases
      this.crafts = result.data.crafts
      this.items = result.data.items
      this.bestItem = result.data.bestItem
      this.user = result.data.user
      this.inventory = result.data.inventory
      this.soldItems = result.data.soldItems
      this.withdrewItems = result.data.withdrewItems
      this.bestCase = result.data.bestCase
      if (!result.data) {
        this.options = []
      } else {
        this.options = result.data.count
        if (result.data.count.length > 0) {
          this.filterItems = result.data.count[0]
        }
      }
    } catch (e) {
      this.showNotification(this.showNotification(this.$t('smtWrong'), 'danger'))
    }
    eventBus.$emit('closeMenu')
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
.customLabel
  display: flex
  align-items: center
  font-weight: 500
  &__name
    color: 14px
    margin-right: 12px
  &__num
    font-size: 13px
    line-height: 16px
    color: #00bbff
    background: white
    padding: 1px 4px
    border-radius: 64px
.dashLinks
  color: rgba(224, 224, 255, 0.6)
  font-size: 14px
  display: flex
  align-items: center
  +sm
    margin-top: 32px
    width: 100%
  &__link
    padding: 6px 16px
    border-radius: 18px
    display: flex
    align-items: center
    cursor: pointer
    font-weight: 500
    user-select: none
    +sm
      width: 50%
      justify-content: center
    &:hover
      color: white
    &_active
      color: white
      +btn_primary
    .text
      margin-left: 10px
.dash
  &__col
    +lg
      margin-bottom: 16px
  &__photo
    display: none
  &__item
    min-height: 160px
    +item_dark
    padding: 32px
    display: flex
    align-items: center
    border-radius: 12px
    height: 100%
    +md
      padding: 20px
    &_between
      justify-content: space-between
    &_wrap
      flex-wrap: wrap
  &__text
    overflow: hidden
  &__block
    display: flex
    align-items: center
    overflow: hidden
  &__balance
    text-transform: uppercase
    font-weight: 600
    font-size: 13px
    line-height: 16px
    color: rgba(224, 224, 255, 0.6)
    letter-spacing: 1px
    margin-bottom: 8px
  &__num
    letter-spacing: -1px
    color: white
    font-size: 24px
    font-weight: bold
    span
      font-size: 13px
  &__img
    margin-right: 32px
    width: 64px
    height: 64px
    border-radius: 50%
    cursor: pointer
    flex-shrink: 0
  &__title
    font-size: 20px
    line-height: 28px
    letter-spacing: -0.4px
    color: white
    font-weight: 600
    margin-bottom: 4px
    white-space: nowrap
    overflow: hidden
    text-overflow: ellipsis
    &_block
      display: block
  &__desc
    font-size: 13px
    line-height: 16px
    color: rgba(224, 224, 255, 0.6)
    font-weight: 500
    padding: 0
    &:hover
      color: white
    &_w100
      width: 100%
    &_photo
      white-space: nowrap
      overflow: hidden
      text-overflow: ellipsis
.dashInfo
  padding: 16px
  color: white
  margin-top: 32px
  border-radius: 12px
  +lg
    margin-top: 16px
  &_green
    background: rgba(0, 255, 170, 0.03)
    .dashInfo__num
      color: #00ffaa
  &_blue
    background: rgba(0, 187, 255, 0.03)
    .dashInfo__num
      color: #00bbff
  &_d-blue
    background: rgba(69, 121, 245, 0.03)
    .dashInfo__num
      color: #4579f5
  &__num
    font-size: 48px
    line-height: 48px
    letter-spacing: -2px
    margin-bottom: 12px
    font-weight: bold
    +md
      font-size: 24px
      line-height: 24px
  &__text
    text-transform: uppercase
    letter-spacing: 1px
    font-size: 14px
    font-weight: 600
    color: rgba(224, 224, 255, 0.6)
</style>
