<template>
  <v-card>
    <ul class="flex flex-col lg:flex-row gap-10">
      <v-col>
        <v-card-title>
          {{ product.name }}
        </v-card-title>
        <v-img
          contain
          max-width="500"
          max-hight="500"
          :src="'https://localhost:44367/Products/Image/' + product.id"
        >
        </v-img>

        <v-card-subtitle class="pb-0">
          Producent: {{ product.producerName }}
        </v-card-subtitle>
      </v-col>
      <v-col class="d-flex justify-end pt-md-15 px-lg-10">
        <v-card width="250">
          <v-list>
            <v-list-item>
              <v-list-item-content>
                <v-row class="d-flex justify-space-between px-6 pt-4"
                  ><v-icon>fas fa-money-bill</v-icon>
                  <v-header>{{ product.price }} zł</v-header>
                </v-row>
              </v-list-item-content>
            </v-list-item>
            <v-list-item>
              <v-list-item-content class="d-flex justify-center mb-6">
                <v-header v-if="product.amount > 0"
                  ><v-icon color="green accent-4">fas fa-check-circle</v-icon>
                  dostępny</v-header
                >
                <v-header v-if="product.amount < 1"
                  ><v-icon color="red accent-4">fas fa-times-circle</v-icon>
                  nie-dostępny</v-header
                >
              </v-list-item-content>
            </v-list-item>
            <v-list-item>
              <v-list-item-content>
                <v-text-field
                  class="mt-0 pt-0"
                  label="ilość"
                  type="number"
                  value="1"
                  style="width: 60px"
                ></v-text-field>
              </v-list-item-content>
            </v-list-item>
            <v-list-item two-line>
              <v-list-item-content>
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
              </v-list-item-content>
            </v-list-item>
          </v-list>
        </v-card>
      </v-col>
    </ul>
    <v-divider></v-divider>
    <v-card-text class="text--primary">
      <v-container fluid>
        <v-textarea
          name="input-7-1"
          filled
          solo
          readonly
          label="opis"
          auto-grow
          :value="product.description"
        ></v-textarea>
      </v-container>
    </v-card-text>
    <v-sheet elevation="8">
      <h2 class="text-center pt-5">You may also want</h2>

      <v-slide-group v-model="model" active-class="success" show-arrows>
        <v-slide-item v-for="n in 3" :key="n" class="ml-5">
          <Product
            :product="recomendedProduct"
            :key="recomendedProduct.id"
            class="ma-5"
          />
        </v-slide-item>
      </v-slide-group>
    </v-sheet>
  </v-card>
</template>
<script>
export default {
  async asyncData({ params, $axios }) {
    const product = await $axios.$get(
      "https://localhost:44367/Products/" + params.slug
    );
    const recomendedProduct = await $axios.$get(
      "https://localhost:44367/Products/" + product.copurchasedProductId
    );
    return { product, recomendedProduct };
  },
  data() {
    return {
      loginDialog: false,
      model: null,
    };
  },
};
</script>
