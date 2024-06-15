<template>
  <v-app dark>
    <Header></Header>
    <v-main>
      <v-container fluid>
        <nuxt />
      </v-container>
    </v-main>
  </v-app>
</template>

<script>
import Header from "../../ClientApp/components/header/Header.vue";
import https from "https";
import axios from "axios";

const agent = new https.Agent({
  rejectUnauthorized: false,
});

axios.defaults.httpsAgent = agent;
export default {
  components: { Header },
};
</script>
