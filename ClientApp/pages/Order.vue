<template>
  <ul class="flex justify-center gap-10">
    <v-col flex-grow="1">
      <v-row
        ><div class="flex flex-col gap-4" flex-grow="1">
          <Order
            v-for="order in orderList.orders"
            :order="order"
            :key="order.id"
          /></div
      ></v-row>
    </v-col>
    <v-col class="flex justify-end align-start px-lg-10">
      <v-card width="250">
        <v-list>
          <v-list-item>
            <v-list-item-content>
              <v-row class="d-flex justify-space-between px-6 pt-4"
                ><v-icon>fas fa-money-bill</v-icon>
                <v-header> {{ orderList.totalPrice }} zł</v-header>
              </v-row>
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
                @click="submitOrder"
              >
                Zapłać
              </v-btn>
            </v-list-item-content>
          </v-list-item>
        </v-list>
      </v-card>
    </v-col>
  </ul>
</template>

<script>
export default {
  data() {
    return {
      products: [],
      orders: [],
      orderList: [],
      active: [],
      page: 1,
    };
  },
  async fetch() {
    this.orderList = await this.$axios.$get("https://localhost:44367/Orders");
  },
  middleware: "auth",
  methods: {
    async refreshOrderList() {
      try {
        this.orderList = await this.$axios.$get(
          "https://localhost:44367/Orders"
        );
      } catch (error) {
        console.error("Error fetching order list:", error);
      }
    },
    async submitOrder() {
      try {
        await this.$axios.$post("https://localhost:44367/Orders/SubmitOrder");
        console.log("Order submitted successfully!");
        await this.refreshOrderList(); // Refresh the order list after successful submission
      } catch (error) {
        console.error("Error submitting order:", error);
      }
    },
  },
};
</script>
