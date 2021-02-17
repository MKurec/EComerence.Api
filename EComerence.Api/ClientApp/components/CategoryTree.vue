<template>
  <v-treeview
    activatable
    :categories="categories"
  ></v-treeview>
</template>
<script>
  export default {
    data: () => {
      return{
        categories:[]
      }
    },
    async fetch() {
      this.categories = await fetch('https://localhost:44367/Categories').then((res) => res.json())
    }
  }
</script>