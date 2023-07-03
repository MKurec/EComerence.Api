<template>
  <v-app-bar dense color="#43a047" dark max-height="50" outlined>
    <template v-slot:img="{ props }">
      <v-img
        v-bind="props"
        gradient="to top right, rgba(55,236,186,.7), rgba(25,32,72,.7)"
      ></v-img>
    </template>

    <v-app-bar-nav-icon></v-app-bar-nav-icon>

    <NuxtLink to="/" no-prefetch>
      <v-app-bar-title class="px-2">Sklep</v-app-bar-title></NuxtLink
    >
    <NuxtLink
      to="/AddProduct/"
      no-prefetch
      v-if="$auth.loggedIn && $auth.user.role == 'admin'"
    >
      <v-app-bar-title>AddProduct</v-app-bar-title></NuxtLink
    >

    <v-spacer></v-spacer>
    <NuxtLink to="/order/" no-prefetch v-if="$auth.loggedIn">
      <v-btn icon v-if="$auth.loggedIn">
        <v-badge color="orange" content="6">
          <v-icon>fas fa-shopping-basket</v-icon>
        </v-badge>
      </v-btn>
    </NuxtLink>
    <v-btn icon v-if="$auth.loggedIn" @click="logout">
      <v-icon>fas fa-sign-out-alt</v-icon>
    </v-btn>

    <div v-if="$auth.loggedIn"></div>
    <div v-else>
      <v-row>
        <v-col class="d-flex align-center pl-6"><Register></Register></v-col>
        <v-col class="d-flex align-start pr-8"
          ><v-btn color="primary" @click.stop="loginDialog = true">
            Zaloguj </v-btn
          ><Login v-model="loginDialog" />
        </v-col>
      </v-row>
    </div>
  </v-app-bar>
</template>
<script>
export default {
  name: "VmHeader",
  methods: {
    async logout() {
      await this.$auth.logout();
    },
  },
  data() {
    return {
      loginDialog: false,
    };
  },
};
</script>