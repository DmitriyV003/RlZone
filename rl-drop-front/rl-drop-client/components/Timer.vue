<template>
  <div>
    <div v-if="statusType !== 'expired'" class="ice__timer">
      <div class="ice__timer-item">
        <div class="num">
          {{ days }}
        </div>
        <div class="day">
          {{ wordString.day }}
        </div>
      </div>
      <div class="ice__timer-item">
        <div class="num">
          {{ hours }}
        </div>
        <div class="day">
          {{ wordString.hours }}
        </div>
      </div>
      <div class="ice__timer-item">
        <div class="num">
          {{ minutes }}
        </div>
        <div class="day">
          {{ wordString.minutes }}
        </div>
      </div>
      <div class="ice__timer-item">
        <div class="num">
          {{ seconds }}
        </div>
        <div class="day">
          {{ wordString.seconds }}
        </div>
      </div>
    </div>
    <div v-else class="ice__timer-item">
      <div class="expired">
        {{ statusType }}
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: ['starttime', 'endtime', 'trans'],
  data () {
    return {
      timer: '',
      wordString: {},
      start: '',
      end: '',
      interval: '',
      days: '',
      minutes: '',
      hours: '',
      seconds: '',
      message: '',
      statusType: '',
      statusText: ''

    }
  },
  watch: {
    endtime () {
      this.updateTimer()
    },
    starttime () {
      this.updateTimer()
    }
  },
  created () {
    this.wordString = JSON.parse(this.trans)
  },
  mounted () {
    this.updateTimer()
  },
  methods: {
    updateTimer () {
      this.start = new Date(this.starttime).getTime()
      this.end = new Date(this.endtime).getTime()
      // Update the count down every 1 second
      this.timerCount(this.start, this.end)
      this.interval = setInterval(() => {
        this.timerCount(this.start, this.end)
      }, 1000)
    },
    timerCount (start, end) {
      // Get todays date and time
      const now = new Date().getTime()

      // Find the distance between now an the count down date
      const distance = start - now
      const passTime = end - now

      if (distance < 0 && passTime < 0) {
        this.message = this.wordString.expired
        this.statusType = 'expired'
        this.statusText = this.wordString.status.expired
        clearInterval(this.interval)
      } else if (distance < 0 && passTime > 0) {
        this.calcTime(passTime)
        this.message = this.wordString.running
        this.statusType = 'running'
        this.statusText = this.wordString.status.running
      } else if (distance > 0 && passTime > 0) {
        this.calcTime(distance)
        this.message = this.wordString.upcoming
        this.statusType = 'upcoming'
        this.statusText = this.wordString.status.upcoming
      }
    },
    calcTime (dist) {
      // Time calculations for days, hours, minutes and seconds
      this.days = Math.floor(dist / (1000 * 60 * 60 * 24))
      this.hours = Math.floor((dist % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60))
      this.minutes = Math.floor((dist % (1000 * 60 * 60)) / (1000 * 60))
      this.seconds = Math.floor((dist % (1000 * 60)) / 1000)
    }

  }

}
</script>
