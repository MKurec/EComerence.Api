
<template>
  <div>
     <ul class="flex flex-col lg:flex-row ">
      <CategoryTree :categories="categories" />
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
      categories:[]
    }
  },
  async fetch() {
    this.products = await fetch('https://localhost:44367/Products').then((res) => res.json())
  },
  async fetch() {
    this.categories = await fetch('https://localhost:44367/Categories').then((res) => res.json())
  }
}
</script> 
