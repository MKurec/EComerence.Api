<template>
  <v-hover>    
    <template v-slot:default="{ hover }">
      <v-card
        :elevation="hover ? 24 : 6"
        class="mx-auto my-12"
        max-width="344"
        outlined
      >
        <v-list-item three-line>
          <v-list-item-content>
            <div class="overline mb-4">
              {{product.name}}
            </div>
            <v-list-item-title class="headline mb-1">
              {{product.producerName}}
            </v-list-item-title>
            <v-list-item-subtitle>{{product.description}}</v-list-item-subtitle>
          </v-list-item-content>

          <v-list-item-avatar
            tile
            size="80"
            color="grey"
          ></v-list-item-avatar>
        </v-list-item>

        <v-card-actions>
          <v-btn
            outlined
            rounded
            text
          >
            {{product.price}}
          </v-btn>
        </v-card-actions>
      </v-card>
    </template>
  </v-hover>
</template>
<script>
export default {
  props: {
    product:{
      type: Object,
      default: () => {}

    }
  }
}
</script>