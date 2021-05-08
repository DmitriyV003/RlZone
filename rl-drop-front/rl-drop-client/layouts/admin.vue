<template>
  <div class="scrollContainer" v-bar>
    <div>
      <b-navbar toggleable="lg" type="dark" variant="info" fixed="top">
        <b-navbar-brand to="/" href="#">RLDrop</b-navbar-brand>

        <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

        <b-collapse id="nav-collapse" is-nav>
          <b-navbar-nav>
            <b-nav-item href="#">Admin Panel</b-nav-item>
<!--            <b-nav-item href="#" disabled>Disabled</b-nav-item>-->
          </b-navbar-nav>

<!--          &lt;!&ndash; Right aligned nav items &ndash;&gt;-->
<!--          <b-navbar-nav class="ml-auto">-->
<!--            <b-nav-form>-->
<!--              <b-form-input size="sm" class="mr-sm-2" placeholder="Search"></b-form-input>-->
<!--              <b-button size="sm" class="my-2 my-sm-0" type="submit">Search</b-button>-->
<!--            </b-nav-form>-->

<!--            <b-nav-item-dropdown text="Lang" right>-->
<!--              <b-dropdown-item href="#">EN</b-dropdown-item>-->
<!--              <b-dropdown-item href="#">ES</b-dropdown-item>-->
<!--              <b-dropdown-item href="#">RU</b-dropdown-item>-->
<!--              <b-dropdown-item href="#">FA</b-dropdown-item>-->
<!--            </b-nav-item-dropdown>-->

<!--            <b-nav-item-dropdown right>-->
<!--              &lt;!&ndash; Using 'button-content' slot &ndash;&gt;-->
<!--              <template v-slot:button-content>-->
<!--                <em>User</em>-->
<!--              </template>-->
<!--              <b-dropdown-item href="#">Profile</b-dropdown-item>-->
<!--              <b-dropdown-item href="#">Sign Out</b-dropdown-item>-->
<!--            </b-nav-item-dropdown>-->
<!--          </b-navbar-nav>-->
        </b-collapse>
      </b-navbar>

      <div class="admin">
        <div class="admin__sidebar">
          <nuxt-link active-class="admin__link_active" tag="div" class="admin__link" to="/admin/items">
            Items
          </nuxt-link>
          <nuxt-link active-class="admin__link_active" tag="div" class="admin__link" to="/admin/add-item">
            Add Item
          </nuxt-link>
          <nuxt-link active-class="admin__link_active" tag="div" class="admin__link" to="/admin/add-type">
            Add Item Type
          </nuxt-link>
          <nuxt-link active-class="admin__link_active" tag="div" class="admin__link" to="/admin/create-case">
            Create Case
          </nuxt-link>
          <nuxt-link active-class="admin__link_active" tag="div" class="admin__link" to="/admin/cases">
            Cases
          </nuxt-link>
          <nuxt-link active-class="admin__link_active" tag="div" class="admin__link" to="/admin/stats/chests">
            Cases Stats
          </nuxt-link>
          <nuxt-link active-class="admin__link_active" tag="div" class="admin__link" to="/admin/create-faq">
            Create faq
          </nuxt-link>
          <nuxt-link active-class="admin__link_active" tag="div" class="admin__link" to="/admin/stats/item-types">
            Item Types Stats
          </nuxt-link>
          <nuxt-link active-class="admin__link_active" tag="div" class="admin__link" to="/admin/stats/craft">
            Craft stats
          </nuxt-link>
          <nuxt-link active-class="admin__link_active" tag="div" class="admin__link" to="/admin/stats/sales">
            Sales
          </nuxt-link>
          <nuxt-link active-class="admin__link_active" tag="div" class="admin__link" to="/admin/users">
            Users
          </nuxt-link>
          <nuxt-link active-class="admin__link_active" tag="div" class="admin__link" to="/admin/poster/top-index">
            Top Index Banner
          </nuxt-link>
          <nuxt-link active-class="admin__link_active" tag="div" class="admin__link" to="/admin/poster/bottom-index">
            Bottom Index Banner
          </nuxt-link>
          <nuxt-link active-class="admin__link_active" tag="div" class="admin__link" to="/admin/withdraws">
            Withdraws
          </nuxt-link>
        </div>

        <div class="admin__content">
          <nuxt />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapMutations } from 'vuex'

export default {
  components: {},
  middleware: ['both'],
  async created () {
    try {
      await this.$store.dispatch('admin/colors/getAllColors')
    } catch (e) {
    }
  },
  mounted () {
    const that = this
    this.windowSize = window.innerWidth
    this.setWindowSize(window.innerWidth)
    window.addEventListener('resize', function () {
      const windowSize = window.innerWidth
      that.setWindowSize(windowSize)
    })
  },
  methods: {
    ...mapMutations({
      setWindowSize: 'common/setWindowSize'
    })
  }
}
</script>

<style lang="sass">
*,
*:before,
*:after
  box-sizing: border-box
  margin: 0
.navbar
  background: #202036 !important
  z-index: 10
.dropzone-custom-title
  color: black
.admin
  display: flex
  margin-top: 75px
  &__sidebar
    width: 350px
    padding: 15px 20px
  &__link
    color: white
    font-size: 18px
    line-height: 24px
    cursor: pointer
    margin-bottom: 10px
    font-weight: 600
    &:hover
      color: rgba(224, 224, 255, 0.6)
    &_active
      color: rgba(224, 224, 255, 0.6)
  &__content
    padding-right: 20px
    width: 100%
</style>
