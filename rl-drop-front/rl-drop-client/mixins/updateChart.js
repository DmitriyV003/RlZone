export default {
  methods: {
    mergeChartOptions (legend, series, chart) {
      chart.mergeOptions({
        legend: {
          data: legend
        },
        series: {
          data: series
        }
      })
    }
  }
}
