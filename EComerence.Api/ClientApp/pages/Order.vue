<template>
  <ul class="flex flex-col gap-10">
    <v-col flex-grow="1">
      <v-row
        ><div class="flex flex-col gap-4" flex-grow="1">
          <Order
            v-for="order in orders"
            :order="order"
            :key="order.id"
          /></div
      ></v-row>
    </v-col>
  </ul>
</template>

<script>
export default {
  data() {
    return {
      products: [],
      orders: [],
      active: [],
      page: 1,
    };
  },
  async fetch() {
    this.orders = await this.$axios.$get("https://localhost:44367/Orders");
  },
};
</script> 
