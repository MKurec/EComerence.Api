<template>
  <v-hover>
      <template v-slot:default="{ hover }">
        <v-card class="mx-auto" width="400" :elevation="hover ? 24 : 6">
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

          <v-card-actions>
            <v-btn outlined rounded color="orange" text> Share </v-btn>

            <v-btn color="orange" text> Explore </v-btn>
          </v-card-actions>
        </v-card>
      </template>
    </v-hover>
</template>
<script>
export default {
   async asyncData({ params, $axios }) {
            const product = await $axios.$get('https://localhost:44367/Products/'+params.slug)
             return { product }
   }
}
</script>
