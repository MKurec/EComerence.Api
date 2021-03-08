<template>
    <v-app-bar
      
      dense
      color="#43a047"
      dark
      max-height=50 
      outlined

    >
      <template v-slot:img="{ props }">
        <v-img
          v-bind="props"
          gradient="to top right, rgba(55,236,186,.7), rgba(25,32,72,.7)"
        ></v-img>
      </template>

      <v-app-bar-nav-icon></v-app-bar-nav-icon>

      <v-app-bar-title>Title</v-app-bar-title>

      <v-spacer></v-spacer>

      <v-btn icon>
        <v-icon>mdi-magnify</v-icon>
      </v-btn>

      <v-btn icon v-if="$auth.loggedIn" @click="logout">
        <v-icon >mdi-heart</v-icon>
      </v-btn>

      <v-btn icon>
        <v-icon>mdi-dots-vertical</v-icon>
      </v-btn>
      <div v-if="$auth.loggedIn"></div>
      <div v-else >
        <v-row >
          <v-col > </v-col>
          <v-col ><Register></Register></v-col>
          <v-col > </v-col>
          <v-col ><Login></Login> </v-col>
        </v-row>
      </div>
    </v-app-bar>
</template>
<script>
  export default {
    name: 'VmHeader', 
    methods: {
    async logout() {
      await this.$auth.logout();
    },
  }  
  };
</script>