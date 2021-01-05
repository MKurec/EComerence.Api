<template>
  <nav class="relative flex flex-wrap items-center justify-between px-2 py-3 navbar-expand-lg bg-blue-500 mb-3">
    <div class="container px-4 mx-auto flex flex-wrap items-center justify-between">
      <div class="w-full relative flex justify-between lg:w-auto  px-4 lg:static lg:block lg:justify-start">
        <a class="text-sm font-bold leading-relaxed inline-block mr-4 py-2 whitespace-no-wrap uppercase text-white" href="#pablo">
          blue Color
        </a>
        <button class="text-white cursor-pointer text-xl leading-none px-3 py-1 border border-solid border-transparent rounded bg-transparent block lg:hidden outline-none focus:outline-none" type="button" v-on:click="toggleNavbar()">
          <i class="fas fa-bars"></i>
        </button>
      </div>
      <div v-bind:class="{'hidden': !showMenu, 'flex': showMenu}" class="lg:flex lg:flex-grow items-end">
        <ul class="flex flex-col lg:flex-row items-center list-none ml-auto ">
          <li class="nav-item">
            <a href="#" class="icon" :title="facebookTooltip">
              <i class="fa fa-facebook"></i>
            </a>
          </li>
          <li class="nav-item">
            <a href="#" class="icon" :title="instagramTooltip">
              <i class="fa fa-instagram"></i>
            </a>
          </li>
          <li class="nav-item ml-2">
            <a href="#" class="icon" @click="showCheckoutModal">
              <span class="icon mr-1">
                <i class="fa fa-shopping-cart"></i>
              </span>
              <span :class="[numProductsAdded > 0 ? 'tag is-info' : ''] ">{{ numProductsAdded }} </span>
            </a>
          </li>
          <li class="nav-item ml-2">
            <VmMenu></VmMenu>
          </li>
        </ul>
      </div>
    </div>
  </nav>
</template>

<script>
  import VmMenu from '../menu/Menu';
  import VmSearch from '../search/Search';

  export default {
    name: 'VmHeader',

    data () {
      return {
        linkedinTooltip: 'Follow us on Linkedin',
        facebookTooltip: 'Follow us on Facebook',
        twitterTooltip: 'Follow us on Twitter',
        instagramTooltip: 'Follow us on Instagram',
        isCheckoutActive: false,
        showMenu: false
      }
    },

    components: {
      VmSearch,
      VmMenu
    },

    computed: {
      numProductsAdded () {
        return this.$store.getters.productsAdded.length;
      }
    },

    methods: {
      showCheckoutModal() {
        this.$store.commit('showCheckoutModal', true);
      },
        toggleNavbar: function () {
          this.showMenu = !this.showMenu;
        }
      }
    
  };
</script>

<style lang="scss" scoped>
  .title {
    background: url('../../static/vuemmerce-logo.png') no-repeat;
    background-position: 50% 50%;
    background-size: 165px;
    width: 175px;
    height: 35px;
  }
  .shopping-cart {
    cursor: pointer;
  }
  a {
    color: whitesmoke;
  }
</style>
