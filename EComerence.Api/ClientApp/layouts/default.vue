<template>
  <v-app dark>
    <Header></Header>
    <v-main>
      <v-container>
        <nuxt />
      </v-container>
    </v-main>
  </v-app>
</template>

<script>
import Header from '../../ClientApp/components/header/Header.vue'
export default {
  components: { Header },
}
</script>
