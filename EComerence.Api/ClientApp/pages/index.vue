
<template>
  <div>
     <ul class="flex flex-col lg:flex-row gap-10 ">
        <v-treeview
        :active.sync="active"
         activatable
        :items="items"
        item-children="subCategories"
        return-object
        ></v-treeview>
      <div class="flex flex-wrap gap-4">
        <Product v-for="product in products" :product="product" :key="product.name" />
      </div>
      <div v-if="active">
        {{active[0].name}}
      </div>
      </ul>
  </div>
</template>

<script>

export default { 
    data() {
    return{
      products:[],
      items:[],
      active: []
    }
  },
  async fetch() {
    this.products = await fetch('https://localhost:44367/Products').then((res) => res.json()),
    this.items = await fetch('https://localhost:44367/Categories').then((res) => res.json())
  }
  
}
</script> 
