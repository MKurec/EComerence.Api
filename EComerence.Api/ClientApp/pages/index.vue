
<template>
  <ul class="flex flex-col lg:flex-row gap-10">
    <v-col style="max-width: 200px " flex-grow=0>
      
      <v-treeview
        style="min-width: 199px"
        :active.sync="active"
        activatable
        :items="items"
        item-children="subCategories"
        return-object
      ></v-treeview>
    </v-col>
    <v-col flex-grow=1>
      <v-row><div class="flex flex-wrap gap-4 " flex-grow=1>
          <Product
            v-for="product in products"
            :product="product"
            :key="product.name"
          />
        </div></v-row>
      
        
      

      <v-row class="align-content-center"><v-pagination :total-visible="7" > </v-pagination></v-row>
    </v-col>
  </ul>
</template>

<script>
export default {
  data() {
    return {
      products: [],
      items: [],
      active: [],
      page: 1,
    };
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
  async fetch() {
    this.items = await fetch(
      "https://localhost:44367/Categories/Tree"
    ).then((res) => res.json());
  },
};
</script> 
