<template>
  <div>
    <div v-if="getWindowSize > 991" class="menu">
      <b-container class="menu__container">
        <div class="menu__content menu__content-1">
          <nuxt-link :to="localePath('/')" class="menu__link menu__logo">
            <img src="/images/logo.svg" alt="">
          </nuxt-link>
          <nuxt-link :to="localePath('/craft')" class="menu__link">
            {{ $t('craft') }}
          </nuxt-link>
          <nuxt-link :to="localePath('/faq')" class="menu__link">
            FAQ
          </nuxt-link>
        </div>
        <div class="menu__content menu__content-2">
          <div @click="setSound" class="menu__icon">
            <span class="menu__volume">
              <VolumeIcon v-if="sound" />
              <VolumeOffIcon v-else />
            </span>
          </div>
          <div class="menu__icon">
            <span @click="showNotifications" class="menu__volume">
              <BellIcon />
              <div
                v-if="getNotifications.length > 0"
                class="notifications__count"
              >{{ getNotifications.length > 9 ? '9+' : getNotifications.length }}
              </div>
            </span>
            <transition name="fade">
              <div v-if="isNotificationShow" @mouseleave="isNotificationShow = false" class="notifications">
                <p class="notifications__title">
                  {{ $t('notifications') }}
                </p>
                <p v-if="getNotifications.length == 0" class="notifications__none">
                  {{ $t('noNotifications') }}
                </p>
                <div v-else>
                  <Notification
                    v-for="(item, index) in notifications"
                    :key="index"
                    :notification-type="item.type.toLowerCase()"
                    :id="item.id"
                    :date="item.date"
                  >
                    <!--                    <p><span class="blue"></span><span class="white"> {{ item.text }}</span></p>-->
                    <p v-html="item[`text_${$i18n.locale}`]" />
                  </Notification>
                  <button v-if="getNotifications.length > 0" @click="readNotifications" class="btn btn_primary notifications__read">Read all</button>
                </div>
              </div>
            </transition>
          </div>
          <div class="menu__langs">
            <div @click="showLangs" class="menu__langs-link">
              <img :src="`/images/${$i18n.locale}.svg `" alt="" class="menu__langs-img">
              <span class="menu__langs-lang">{{ $i18n.locale }}</span>
            </div>
            <div v-if="isLangsShow" @mouseleave="isLangsShow = false" @mouseover="isLangsShow = true" class="menu__langs-drop">
              <nuxt-link
                v-for="locale in $i18n.locales"
                :key="locale.code"
                :to="switchLocalePath(locale.code)"
                tag="div"
                class="menu__langs-dropLink"
              >
                <img :src="`/images/${locale.code}.svg `" alt="" class="menu__langs-img">
                <span class="menu__langs-dropLang">{{ locale.name }}</span>
                <CheckIcon v-if="locale.code === $i18n.locale" class="icon" />
              </nuxt-link>
            </div>
          </div>

          <div v-if="getToken && getUser" class="menu__langs">
            <div @click="showFinancial" class="menu__langs-link">
              <CardIcon class="menu__langs-icon" />
              <span class="munu__langs-lang">${{ Number(getUser.balance.toFixed(2)) }}</span>
            </div>
            <div v-if="isFinancialShow" @mouseleave="isFinancialShow = false" class="financial">
              <div class="financial__top">
                <span class="financial__title">{{ $t('balance') }}</span>
                <span class="financial__number">{{ Number(getUser.balance.toFixed(2)) }} <span>USD</span></span>
              </div>
              <div class="financial__btn">
                <button class="btn btn_gray">
                  {{ $t('deposit') }}
                </button>
              </div>
            </div>
          </div>

          <div v-if="getToken && getUser" class="account">
            <div @click="showAccount" class="account__icon">
              <img v-if="getUser.photo" :src="getUser.photo" alt="" class="account__img">
              <AccountEmpty v-else class="account__img account__img_icon" />
              <ArrowDownIcon :class="isAccountShow ? 'account__arrow_rotate' : ''" class="account__arrow" />
            </div>
            <div v-if="isAccountShow" @mouseleave="isAccountShow = false" class="account__drop">
              <p v-if="getToken && getUser" class="account__name">
                {{ getUser.name }}
              </p>
              <nuxt-link class="account__link" to="/dashboard" tag="div">
                <Dashboard class="icon" />
                <span>{{ $t('dashboard') }}</span>
              </nuxt-link>
              <nuxt-link class="account__link" to="/settings" tag="div">
                <SettingsIcon class="icon" />
                <span>{{ $t('settings') }}</span>
              </nuxt-link>
              <button @click.prevent="logout" class="account__link account__link_logout">
                <LogoutIcon class="icon" />
                <span>{{ $t('signOut') }}</span>
              </button>
            </div>
          </div>
          <nuxt-link v-if="!getToken" to="/sign-in" tag="button" class="menu__btn btn btn_primary">
            {{ $t('getStarted') }}
          </nuxt-link>
        </div>
      </b-container>
    </div>

    <div v-if="getWindowSize <= 991">
      <!-- Mobile main menu -->
      <div v-if="isMobileMainMenuShow">
        <div class="mobileMenu">
          <div class="mobileMenu__visible">
            <div @click="showDropMenu = true" v-if="!showDropMenu" class="mobileMenu__item">
              <MenuIcon class="mobileMenu__icon" />
            </div>
            <div v-if="showDropMenu" @click="showDropMenu = false" class="mobileMenu__item">
              <CloseIcon class="mobileMenu__icon" />
            </div>
            <div class="mobileMenu__item mobileMenu__item_center">
              <nuxt-link :to="localePath('/')">
                <img src="/images/logo-sm.svg" alt="">
              </nuxt-link>
            </div>
            <nuxt-link v-if="!getToken" :to="localePath('/sign-up')" tag="div" class="mobileMenu__item mobileMenu__item_right">
              <span class="mobileMenu__btn">{{ $t('getStarted') }}</span>
            </nuxt-link>

            <div v-if="getToken && getUser" class="mobileMenu__balance">
              ${{ getUser.balance }}
            </div>
          </div>

          <div v-if="showDropMenu" class="mobileMenu__invisible">
            <nuxt-link :to="localePath('/')" tag="div" class="mobileMenu__link mobileMenu__link_pt12 mobileMenu__link_pl72">
              {{ $t('home') }}
            </nuxt-link>
            <nuxt-link :to="localePath('/craft')" tag="div" class="mobileMenu__link mobileMenu__link_pt12 mobileMenu__link_pl72">
              {{ $t('craft') }}
            </nuxt-link>
            <div @click="showLangs" class="mobileMenu__link mobileMenu__link_pt12 mobileMenu__link_withIcon">
              <img src="/images/russia.svg" alt="" class="mobileMenu__icon mobileMenu__img">
              <span>{{ $t('language') }}</span>
              <div class="mobileMenu__go">
                <span>{{ $i18n.locale }}</span>
                <ArrowRIcon class="arrow" />
              </div>
            </div>
            <div v-if="getToken && getUser" @click="showFinancial" class="mobileMenu__link mobileMenu__link_pt12 mobileMenu__link_withIcon">
              <CardIcon class="mobileMenu__icon mobileMenu__icon_dark" />
              <span>{{ $t('balance') }}</span>
              <div class="mobileMenu__go">
                <span>{{ Number(getUser.balance.toFixed(2)) }}</span>
                <ArrowRIcon class="arrow" />
              </div>
            </div>
            <div @click="showMobileNotifications" class="mobileMenu__link mobileMenu__link_pt12 mobileMenu__link_withIcon">
              <BellIcon class="mobileMenu__icon mobileMenu__icon_dark" />
              <span>{{ $t('notifications') }}</span>
              <div v-if="getNotifications.length > 0" class="mobileMenu__go">
                <span class="notificationsMobile">{{ getNotifications.length > 9 ? '9+' : getNotifications.length }}</span>
                <ArrowRIcon class="arrow" />
              </div>
            </div>
            <div @click="setSound" class="mobileMenu__link mobileMenu__link_pt12 mobileMenu__link_withIcon">
              <VolumeIcon v-if="sound" class="mobileMenu__icon mobileMenu__icon_dark" />
              <VolumeOffIcon v-else class="mobileMenu__icon mobileMenu__icon_dark" />
              <span>Sound</span>
              <div class="mobileMenu__go">
                <div :class="sound ? 'toggler_active' : ''" class="toggler">
                  <label class="toggler__label">
                    <div class="toggler__circle" />
                    <div class="toggler__way" />
                  </label>
                </div>
              </div>
            </div>

            <div v-if="getToken && getUser" @click="showAccount" class="mobileMenu__link mobileMenu__link_pt12 mobileMenu__link_withIcon">
              <img v-if="getUser.photo" :src="getUser.photo" alt="" class="mobileMenu__icon mobileMenu__img">
              <AccountEmpty v-else class="mobileMenu__icon account__img_icon" />
              <span>{{ $t('account') }}</span>
              <div class="mobileMenu__go">
                <span>{{ getUser.name }}</span>
                <ArrowRIcon class="arrow" />
              </div>
            </div>
            <nuxt-link :to="localePath('/sign-up')" v-if="!getToken" tag="button" class="btn btn_primary mobileMenu__start">
              {{ $t('getStarted') }}
            </nuxt-link>
          </div>
        </div>
      </div>

      <div v-if="isFinancialShow" class="mobileMenu__wrapper">
        <div class="mobileMenu__top">
          <div @click="goBack" class="mobileMenu__subItem mobileMenu__subItem_blue">
            <ArrowLIcon class="icon" />
            <span>{{ $t('back') }}</span>
          </div>
          <div class="mobileMenu__subItem mobileMenu__subItem_center">
            {{ $t('balance') }}
          </div>
        </div>
        <button class="btn btn_gray mobileMenu__deposit">
          {{ $t('deposit') }}
        </button>
      </div>

      <div v-if="isMobileNotificationShow" class="mobileMenu__wrapper">
        <div class="mobileMenu__top">
          <div @click="goBack" class="mobileMenu__subItem mobileMenu__subItem_blue">
            <ArrowLIcon class="icon" />
            <span>{{ $t('notifications') }}</span>
          </div>
        </div>
        <p v-if="getNotifications.length === 0" class="notifications__none">
          {{ $t('noNotifications') }}
        </p>
        <div v-else>
          <Notification
            v-for="(item, index) in notifications"
            :key="index"
            :notification-type="item.type.toLowerCase()"
            :id="item.id"
            :date="item.date"
          >
            <p v-html="item[`text_${$i18n.locale}`]" />
          </Notification>
          <button v-if="getNotifications.length > 0" @click="readNotifications" class="btn btn_primary notifications__read">Read all</button>
        </div>
      </div>

      <div v-if="isAccountShow" class="mobileMenu__wrapper">
        <div class="mobileMenu__top">
          <div @click="goBack" class="mobileMenu__subItem mobileMenu__subItem_blue">
            <ArrowLIcon class="icon" />
            <span>{{ $t('back') }}</span>
          </div>
          <div class="mobileMenu__subItem mobileMenu__subItem_center">
            {{ $t('account') }}
          </div>
        </div>
        <nuxt-link tag="div" to="/Dashboard" class="mobileMenu__link mobileMenu__link_pt12 mobileMenu__link_withIcon">
          <Dashboard class="mobileMenu__icon mobileMenu__icon_dark" />
          <span>{{ $t('dashboard') }}</span>
        </nuxt-link>
        <nuxt-link tag="div" to="/settings" class="mobileMenu__link mobileMenu__link_pt12 mobileMenu__link_withIcon">
          <SettingsIcon class="mobileMenu__icon mobileMenu__icon_dark" />
          <span>{{ $t('settings') }}</span>
        </nuxt-link>
        <div @click.prevent="logout" class="mobileMenu__link mobileMenu__link_pt12 mobileMenu__link_withIcon mobileMenu__link_logOut">
          <LogoutIcon class="mobileMenu__icon mobileMenu__icon_dark" />
          <span>{{ $t('signOut') }}</span>
        </div>
      </div>

      <div v-if="isLangsShow" class="mobileMenu__wrapper">
        <div class="mobileMenu__top">
          <div @click="goBack" class="mobileMenu__subItem mobileMenu__subItem_blue">
            <ArrowLIcon class="icon" />
            <span>{{ $t('back') }}</span>
          </div>
          <div class="mobileMenu__subItem mobileMenu__subItem_center">
            {{ $t('language') }}
          </div>
          <div @click="goBack" class="mobileMenu__subItem mobileMenu__subItem_blue mobileMenu__subItem_right">
            <span>{{ $t('done') }}</span>
          </div>
        </div>
        <nuxt-link
          v-for="locale in $i18n.locales"
          :key="locale.code"
          :to="switchLocalePath(locale.code)"
          tag="div"
          class="mobileMenu__link mobileMenu__link_pt12 mobileMenu__link_withIcon"
        >
          <img src="/images/russia.svg" alt="" class="mobileMenu__icon mobileMenu__img">
          <span>{{ locale.name }}</span>
          <div class="mobileMenu__go">
            <CheckIcon v-if="locale.code === $i18n.locale" class="arrow arrow_blue" />
          </div>
        </nuxt-link>
      </div>
    </div>

    <div v-if="getWindowSize <= 991" class="mobileMenu mobileMenu_empty">
      <div class="mobileMenu__visible">
        <div class="mobileMenu__item">
          <MenuIcon class="mobileMenu__icon" />
        </div>
        <div class="mobileMenu__item">
          <CloseIcon class="mobileMenu__icon" />
        </div>
        <div class="mobileMenu__item mobileMenu__item_center">
          <nuxt-link to="/">
            <img src="/images/logo-sm.svg" alt="">
          </nuxt-link>
        </div>
        <div class="mobileMenu__item mobileMenu__item_right">
          <span class="mobileMenu__btn">{{ $t('getStarted') }}</span>
        </div>
      </div>
    </div>
  </div>
</template>

<style lang="sass">
@import '@/theme/_mix.sass'
.menu
  box-shadow: 0 2px 4px -1px rgba(20, 16, 41, 0.24), 0 0 1px 0 rgba(20, 16, 41, 0.4)
  padding: 12px 0
  background: #27273e
  &__logo
    img
      width: 125px
  &__content
    display: flex
    align-items: center
  &__container
    display: flex
    align-items: center
    justify-content: space-between
  &__link
    color: white
    font-weight: 500
    &:hover
      color: rgba(224, 224, 255, 0.6)
    &:not(:last-child)
      margin-right: 48px
  &__volume
    color: rgba(224, 224, 255, 0.6)
    cursor: pointer
  &__icon
    display: flex
    width: 48px
    height: 48px
    justify-content: center
    align-items: center
    margin-right: 12px
    position: relative
    font-size: 25px
  &__langs
    margin-right: 12px
    position: relative
    &-icon
      margin-right: 12px
      font-size: 20px
      line-height: 20px
      height: 14px
    &-img
      margin-right: 12px
    &-lang
      text-transform: uppercase
      color: white
    &-link
      color: white
      font-weight: 600
      display: flex
      align-items: center
      background: rgba(224, 224, 255, 0.02)
      border-radius: 24px
      padding: 12px
      cursor: pointer
      &:hover
        color: rgba(224, 224, 255, 0.6)
    &-drop
      border-radius: 12px
      box-shadow: 0 16px 16px -4px rgba(20, 16, 41, 0.24), 0 4px 8px -1px rgba(20, 16, 41, 0.24), 0 0 1px 0 rgba(20, 16, 41, 0.4)
      background-color: #27273e
      position: absolute
      bottom: 0
      right: 0
      min-width: 255px
      padding: 16px 0
      transform: translateY(calc(100% + 8px))
      z-index: 55
    &-dropLink
      padding: 10px 20px
      color: white
      cursor: pointer
      display: flex
      align-items: center
      justify-content: space-between
      font-size: 14px
      font-weight: 500
      &:hover
        background: rgba(224, 224, 255, 0.02)
      .icon
        color: #00bbff
    &-dropLang
      margin-right: auto
.notifications
  position: absolute
  right: 0
  bottom: 0
  transform: translateY(calc(100% + 8px))
  min-width: 328px
  border-radius: 12px
  box-shadow: 0 16px 16px -4px rgba(20, 16, 41, 0.24), 0 4px 8px -1px rgba(20, 16, 41, 0.24), 0 0 1px 0 rgba(20, 16, 41, 0.4)
  z-index: 55
  background-color: #27273e
  padding: 16px 0
  &__read
    width: calc(100% - 48px)
    margin: 16px 24px 0 24px
  &__none
    color: rgba(224,224,255,.6)
    font-weight: 600
    font-size: 16px
    width: 100%
    text-align: center
  &__count
    font-size: 13px
    line-height: 16px
    color: white
    background: #00bbff
    position: absolute
    top: 0
    right: 0
    padding: 1px 4px
    border-radius: 12px
  &__title
    text-transform: uppercase
    margin-bottom: 24px
    font-size: 14px
    font-weight: 600
    color: rgba(224, 224, 255, 0.6)
    letter-spacing: 1px
    padding: 0 16px
.financial
  border-radius: 12px
  box-shadow: 0 16px 16px -4px rgba(20, 16, 41, 0.24), 0 4px 8px -1px rgba(20, 16, 41, 0.24), 0 0 1px 0 rgba(20, 16, 41, 0.4)
  background-color: #27273e
  min-width: 255px
  padding: 16px
  z-index: 55
  position: absolute
  right: 0
  bottom: 0
  transform: translateY(calc(100% + 8px))
  &__title
    text-transform: uppercase
    font-size: 14px
    letter-spacing: 1px
    color: rgba(224, 224, 255, 0.6)
    font-weight: 600
  &__top
    display: flex
    align-items: center
    justify-content: space-between
    margin-bottom: 24px
  &__number
    color: white
    font-weight: bold
    font-size: 18px
    span
      text-transform: uppercase
      font-size: 12px
  &__btn
    width: 100%
    .btn
      width: 100%
.account
  position: relative
  &__icon
    display: flex
    align-items: center
    color: rgba(224, 224, 255, 0.4)
    font-size: 21px
    cursor: pointer
  &__img
    width: 48px
    height: 48px
    border-radius: 50%
    flex-shrink: 0
    margin-right: 8px
    &_icon
      width: 24px
      height: 24px
      font-size: 30px
      color: rgba(224, 224, 255, 0.4) !important
  &__drop
    border-radius: 12px
    box-shadow: 0 16px 16px -4px rgba(20, 16, 41, 0.24), 0 4px 8px -1px rgba(20, 16, 41, 0.24), 0 0 1px 0 rgba(20, 16, 41, 0.4)
    background-color: #27273e
    z-index: 55
    position: absolute
    right: 0
    bottom: 0
    transform: translateY(calc(100% + 8px))
    padding: 16px 0
    width: 255px
  &__name
    padding: 0 16px
    text-transform: uppercase
    font-size: 14px
    color: rgba(224, 224, 255, 0.6)
    font-weight: 600
    margin-bottom: 24px
  &__arrow
    transition: all 0.2s
    &_rotate
      transform: rotate(180deg)
  &__link
    padding: 9px 20px
    color: white
    font-size: 14px
    font-weight: 500
    cursor: pointer
    user-select: none
    &_logout
      margin-top: 16px
      background: none
      border: none
      width: 100%
      justify-content: flex-start
      display: flex
      align-items: center
    &:hover
      background: rgba(224, 224, 255, 0.02)
    .icon
      color: #e0e0ff
      opacity: 0.6
      margin-right: 5px
.mobileMenu
  padding: 6px 16px
  box-shadow: 0 2px 4px -1px rgba(20, 16, 41, 0.24), 0 0 1px 0 rgba(20, 16, 41, 0.4)
  background-color: #27273e
  position: fixed
  top: 0
  left: 0
  width: 100%
  z-index: 555
  &_empty
    position: initial
    opacity: 0 !important
  &__balance
    color: white
    margin-left: auto
    font-weight: 500
  &__wrapper
    background-color: #27273e
    border-radius: 12px
    +item_dark
    padding: 0 16px 24px 16px
  &__invisible
    padding: 24px 0
  &__visible
    display: flex
    align-items: center
  &__item
    width: 33.333%
    display: flex
    &_center
      align-items: center
      justify-content: center
    &_right
      align-items: center
      justify-content: flex-end
  &__start
    margin-top: 24px
    width: 100%
    margin-left: auto
    margin-right: auto
    display: block
  &__btn
    color: white
    font-weight: 500
    font-size: 14px
    cursor: pointer
  &__icon
    color: white
    font-size: 25px
    cursor: pointer
    height: 20px
    margin-right: 16px
    &_dark
      color:  rgba(224, 224, 255, 0.4)
  &__link
    color: white
    cursor: pointer
    font-weight: 500
    font-size: 14px
    padding-right: 16px
    user-select: none
    &_pt12
      padding-top: 12px
      padding-bottom: 12px
    &_pl72
      padding-left: 56px
    &_withIcon
      display: flex
      align-items: center
      padding-left: 16px
    &_logOut
      margin-top: 16px
  &__go
    display: flex
    align-items: center
    margin-left: auto
    color: rgba(224, 224, 255, 0.6)
    text-transform: uppercase
    .arrow
      font-size: 22px
      height: 18px
      width: 24px
      margin-left: 24px
      &_blue
        color: #00bbff
    .notificationsMobile
      font-size: 13px
      line-height: 16px
      color: white
      font-weight: 500
      padding: 4px
      border-radius: 64px
      background: #00bbff
  &__img
    width: 24px
    height: 24px
    border-radius: 50%
    flex-shrink: 0
  &__top
    display: flex
    align-items: center
    padding: 12px 0
    font-size: 16px
    font-weight: 500
    margin-bottom: 16px
  &__subItem
    width: 33.33333%
    display: flex
    align-items: center
    cursor: pointer
    color: white
    .icon
      font-size: 20px
      height: 15px
    &_blue
      color: #00bbff
    &_center
      justify-content: center
      font-weight: 600
    &_right
      justify-content: flex-end
  &__deposit
    margin: 8px 8px 0 8px
    width: calc(100% - 16px)
</style>

<script>
import showNotification from '@/mixins/showNotification'
import socket from '@/mixins/socket'
import VolumeIcon from 'vue-material-design-icons/VolumeHigh.vue'
import VolumeOffIcon from 'vue-material-design-icons/VolumeOff.vue'
import AccountEmpty from 'vue-material-design-icons/AccountCircleOutline.vue'
import BellIcon from 'vue-material-design-icons/BellOutline.vue'
import CheckIcon from 'vue-material-design-icons/Check.vue'
import CardIcon from 'vue-material-design-icons/CreditCardOutline.vue'
import ArrowDownIcon from 'vue-material-design-icons/ChevronDown.vue'
import Dashboard from 'vue-material-design-icons/ViewDashboardOutline.vue'
import SettingsIcon from 'vue-material-design-icons/CogOutline.vue'
import LogoutIcon from 'vue-material-design-icons/Logout.vue'
import MenuIcon from 'vue-material-design-icons/Menu.vue'
import ArrowRIcon from 'vue-material-design-icons/ChevronRight.vue'
import ArrowLIcon from 'vue-material-design-icons/ChevronLeft.vue'
import CloseIcon from 'vue-material-design-icons/Close.vue'
import { mapGetters } from 'vuex'
import Notification from '../components/Notification'
import { eventBus } from '../plugins/event-bus.js'
export default {
  components: {
    VolumeIcon,
    BellIcon,
    CheckIcon,
    Notification,
    CardIcon,
    ArrowDownIcon,
    Dashboard,
    SettingsIcon,
    LogoutIcon,
    MenuIcon,
    ArrowRIcon,
    CloseIcon,
    ArrowLIcon,
    VolumeOffIcon,
    AccountEmpty
  },
  mixins: [showNotification, socket],
  data () {
    return {
      isLangsShow: false,
      isNotificationShow: false,
      isAccountShow: false,
      isMobileNotificationShow: false,
      isFinancialShow: false,
      showDropMenu: false,
      isMobileMainMenuShow: true,
      sound: false
    }
  },
  computed: {
    ...mapGetters({
      getWindowSize: 'common/getWindowSize',
      getNotifications: 'notifications/getNotifications',
      getToken: 'getToken',
      getUser: 'user/getUser'
    }),
    notifications () {
      return this.getNotifications.slice(0, 4)
    }
  },
  created () {
    eventBus.$on('closeNotification', (id) => {
      this.$store.commit('notifications/deleteNotification', id)
    })
  },
  mounted () {
    if (this.getUser) {
      // window.Echo.private(`room.${this.getUser.id}`)
      //   .listen('CreateNotification', (e) => {
      //     // this.message = e.message
      //     console.log((e))
      //     this.$store.commit('notifications/addNotification', e.notification)
      //   })
      this.privateRoomConnect()
    }
    eventBus.$on('closeMenu', () => {
      this.isLangsShow = false
      this.isNotificationShow = false
      this.isAccountShow = false
      this.isMobileMainMenuShow = true
      this.showDropMenu = false
      this.isFinancialShow = false
      this.isMobileNotificationShow = false
    })
  },
  methods: {
    setSound () {
      this.sound = !this.sound
    },
    async readNotifications () {
      try {
        await this.$store.dispatch('notifications/readAll')
      } catch (e) {

      }
    },
    async logout () {
      try {
        await this.$store.dispatch('logOut')
        this.isLangsShow = false
        this.isNotificationShow = false
        this.isAccountShow = false
        this.isFinancialShow = false
        this.isMobileNotificationShow = false
        this.isMobileNotificationShow = false
      } catch (e) {
      }
    },
    showLangs () {
      this.isLangsShow = this.isLangsShow !== true
      this.isNotificationShow = false
      this.isAccountShow = false
      this.isFinancialShow = false
      this.isMobileMainMenuShow = false
      this.isMobileNotificationShow = false
    },
    showNotifications () {
      this.isLangsShow = false
      this.isNotificationShow = this.isNotificationShow !== true
      this.isAccountShow = false
      this.isFinancialShow = false
      this.isMobileMainMenuShow = false
      this.isMobileNotificationShow = false
    },
    showAccount () {
      this.isLangsShow = false
      this.isNotificationShow = false
      this.isAccountShow = this.isAccountShow !== true
      this.isFinancialShow = false
      this.isMobileMainMenuShow = false
      this.isMobileNotificationShow = false
    },
    showFinancial () {
      this.isLangsShow = false
      this.isNotificationShow = false
      this.isAccountShow = false
      this.isMobileMainMenuShow = false
      this.isFinancialShow = this.isFinancialShow !== true
    },
    showMobileNotifications () {
      this.isMobileNotificationShow = this.isMobileNotificationShow !== true
      this.isLangsShow = false
      this.isNotificationShow = false
      this.isAccountShow = false
      this.isMobileMainMenuShow = false
    },
    goBack () {
      this.isLangsShow = false
      this.isNotificationShow = false
      this.isAccountShow = false
      this.isMobileMainMenuShow = true
      this.isFinancialShow = false
      this.isMobileNotificationShow = false
    }
  }
}
</script>
