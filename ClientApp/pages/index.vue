
<template>
  <v-row>
    <v-col style="max-width: 200px">
      <v-treeview
        style="min-width: 199px"
        :active.sync="active"
        activatable
        :items="items"
        item-children="subCategories"
        return-object
      ></v-treeview>
    </v-col>
    <v-col>
      <v-row
        ><div class="container">
          <Product
            v-for="product in products"
            :product="product"
            :key="product.id"
          /></div
      ></v-row>

      <v-row class="align-content-center"
        ><v-pagination :total-visible="7"> </v-pagination
      ></v-row>
    </v-col>
  </v-row>
</template>

<script>
import Product from "@/components/Product.vue";

export default {
  data() {
    return {
      products: [],
      items: [],
      active: [],
      page: 1,
    };
  },
  components: {
    Product,
  },
  watch: {
    active: async function () {
      if (this.active[0] != null) {
        let products = await this.$axios.$get(
          "https://localhost:44367/Products/Category/" + this.active[0].id
        );
        this.products = products;
      } else {
        let products = await this.$axios.$get(
          "https://localhost:44367/Products"
        );
        this.products = products;
      }
    },
  },
  async created() {
    this.items = await fetch("https://localhost:44367/Categories/Tree").then(
      (res) => res.json()
    );
    if (this.active[0] != null) {
      let products = await this.$axios.$get(
        "https://localhost:44367/Products/Category/" + this.active[0].id
      );
      this.products = products;
    } else {
      let products = await this.$axios.$get("https://localhost:44367/Products");
      this.products = products;
    }
  },
};
</script>
<style scoped>
.container {
  display: flex;
  justify-content: space-between;
  flex-wrap: wrap;
  gap: 20px; /* Adjust this value to change the space between your products */
}
</style>
