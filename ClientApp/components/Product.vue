<template>
  <v-hover>
    <template v-slot:default="{ hover }">
      <v-card class="mx-auto" width="400" :elevation="hover ? 10 : 4">
        <NuxtLink :to="'/Product/' + product.id">
          <v-img
            class="white--text align-end"
            max-height="200px"
            :src="'https://localhost:44367/Products/Image/' + product.id"
          >
            <v-card-title>{{ product.name }}</v-card-title>
          </v-img>

          <v-card-subtitle class="pb-0">
            {{ product.producerName }}
          </v-card-subtitle>

          <v-card-text class="text--primary">
            <div>{{ product.description }}</div>
          </v-card-text>
        </NuxtLink>
        <v-card-actions>
          <v-btn
            outlined
            rounded
            color="orange"
            text
            v-if="$auth.loggedIn"
            @click="addToOrder"
          >
            Dodaj do koszyka
          </v-btn>
          <v-btn
            outlined
            rounded
            color="orange"
            text
            v-if="!$auth.loggedIn"
            @click.stop="loginDialog = true"
          >
            Dodaj do koszyka
          </v-btn>
          <Login v-model="loginDialog" />
          <v-spacer></v-spacer>
          <v-btn text disabled> {{ product.price }} zł </v-btn>
        </v-card-actions>
      </v-card>
    </template>
  </v-hover>
</template>
<script>
import Login from "./Login.vue";
export default {
  components: { Login },
  props: {
    product: {
      type: Object,
      default: () => {},
    },
  },
  data() {
    return {
      loginDialog: false,
    };
  },
  methods: {
    async addToOrder() {
      await this.$axios.post("https://localhost:44367/Orders", {
        productId: this.product.id,
        amount: "1",
      });
    },
  },
};
</script>
