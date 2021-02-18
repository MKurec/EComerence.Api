
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
      <div class="flex flex-wrap gap-4" )>
        <Product v-for="product in products" :product="product" :key="product.name" />
        fetchProducts
      </div>
      <div v-if="active[0]">
                <div ></div>
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
      items:[],
      active: []
    }
  },
  computed: {
    async fetchProducts({$axios,active}){
      if(active[0] != null) {
        const products = await $axios.$get('https://localhost:44367/Products/Category/',{params :{active[0].id}})
        this.products = products
      }
      else{
        const products = await $axios.$get('https://localhost:44367/Products')
        this.products = products
      }
    }

  },
  async fetch() {
    this.items = await fetch('https://localhost:44367/Categories').then((res) => res.json())
  }
  
}
</script> 
