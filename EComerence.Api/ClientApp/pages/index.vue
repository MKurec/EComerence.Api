
<template>
  <div>
     <ul class="flex flex-col lg:flex-row ">
        <v-treeview
         activatable
        :items="items"
        item-children="subCategories"
        ></v-treeview>
      <div class="grid grid-flow-col auto-cols-max md:auto-cols-min">
        <Product v-for="product in products" :product="product" :key="product.name" />
      </div>
      </ul>
  </div>
</template>

<script>

export default { 
    data() {
    return{
      products:[],
      items:[]
    }
  },
  async fetch() {
    this.products = await fetch('https://localhost:44367/Products').then((res) => res.json()),
    this.items = await fetch('https://localhost:44367/Categories').then((res) => res.json())
  }
  
}
</script> 
