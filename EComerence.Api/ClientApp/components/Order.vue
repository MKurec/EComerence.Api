<template>
  <v-hover>
    <template v-slot:default="{ hover }">
      <v-card  width="400" :elevation="hover ? 10 : 4">
       <div class="flex flex-row justify-space-between py-6">
          <v-img
          contain
            class="white--text "
            height="100px"
            maxWidth="200px"
            :src="'https://localhost:44367/Products/Image/' + order.productId"
          >
          </v-img>

          <div class="flex justify-center align-center">
            {{ order.productName }}
          </div>
       </div>
          

        <v-card-actions>
          <v-row class="flex-row  align-center ">
            
            <div class="mx-5"> Cena: {{ order.price }}</div>
            <v-spacer></v-spacer>
          
            <div class="flex flex-row align-center">
            <v-btn icon><v-icon  class="mx-1" color="red accent-4">fas fa-minus-square</v-icon></v-btn>
            <v-text-field

                  label="ilość"
                  readonly
                  type="number"
                  :value=order.amount
                  style="max-width: 30px "
                ></v-text-field>
            <v-btn icon><v-icon class="mx-1" color="green accent-4">fas fa-plus-square</v-icon> </v-btn>
            </div>
          </v-row>
        </v-card-actions>
      </v-card>
    </template>
  </v-hover>
</template>
<script>
export default {
  props: {
    order: {
      type: Object,
      default: () => {},
    },
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