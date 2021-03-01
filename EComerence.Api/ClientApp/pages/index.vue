
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
        <v-hover>
        <Product v-for="product in products" :product="product" :key="product.name" />
        </v-hover>
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
  watch:{
    active: async function(){
      if(this.active[0] != null) {
        let products = await this.$axios.$get('https://localhost:44367/Products/Category/'+this.active[0].id)
        this.products = products
      }
      else{
        let products = await this.$axios.$get('https://localhost:44367/Products')
        this.products = products
      }
    }
  },
  async fetch() {
    this.items = await fetch('https://localhost:44367/Categories').then((res) => res.json())
  }
  
}
</script> 
